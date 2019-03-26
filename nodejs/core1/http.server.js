const http = require('http');
const url = require('url'); // load url resolution/parsing interaction module
const path = require('path'); // load dir interaction module
const fs = require('fs'); // load file system interaction module


const mimeTypes = {
    '.html': 'text/html',
    '.js': 'text/javascript',
    '.css': 'text/css',
    '.ico': 'image/x-icon',
    '.png': 'image/png',
    '.jpg': 'image/jpeg',
    '.gif': 'image/gif',
    '.svg': 'image/svg+xml',
    '.json': 'application/json',
    '.woff': 'font/woff',
    '.woff2': 'font/woff2'
}

var parseRequest = function (req) {
    console.log('server.parseRequest. enter.');

    const parsedUrl = new URL(req.url, 'https://node-http.glitch.me/');
    let pathName = parsedUrl.pathname;
    let ext = path.extname(pathName);

    console.log(`server.parseRequest. info: url=${req.url}; parsedUrl=${parsedUrl}; pathName=${pathName}; ext=${ext}`);

    if (pathName !== '/' && pathName[pathName.length - 1] === '/') {
        console.log('server.parseRequest. section 1.');
        return { Code: 1, Location: pathName.slice(0, -1) };
    }

    if (pathName === '/') {
        console.log('server.parseRequest. section 2.');
        ext = '.html';
        pathName = '/index.html';
    }
    else if (!ext) {
        console.log('server.parseRequest. section 3.');
        ext = '.html';
        pathName += ext;
    }

    const filePath = path.join(process.cwd(), '/public', pathName);

    console.log(`Checking file with path=${filePath}`);


    const stream = fs.createReadStream(filePath);
    return { Code: 3, Headers: { 'Content-Type': mimeTypes[ext] }, Content: stream };

    console.log('server.parseRequest. exit.');
    return { Code: 0 };
}

function getServer(port) {
    var server = http.createServer();

    server.on('request', (req, res) => {
        console.log(`on.request.enter`)

        var result = parseRequest(req);

        console.log(`result code is ${result.Code}`)

        switch (result.Code) {
            case 1:
                res.writeHead(302, { 'Content-Type': 'text/plain', 'Location': result.Location });
                res.end('{ status: true }');
                break;
            case 2:
                res.writeHead(404, { 'Content-Type': 'text/plain', 'Location': result.Location });
                res.end('404 not found');
                break;
            case 3:
                console.log(`on.request 200`)

                res.writeHead(200, result.Headers);
                result.Content.pipe(res);

                break;
            default:
                res.writeHead(200, { 'Content-Type': 'text/plain' });
                res.end('{ status: true }');
                break;
        }
        console.log(`on.request.exit`)
    })

    server.listen(port);

    return server;
}


module.exports = getServer;
var express = require('express');
var app = express();




const protoLoader = require('@grpc/proto-loader');
const grpc = require('grpc');

const options = {
    keepCase: true,
    longs: String,
    enums: String,
    defaults: true,
    oneofs: true
}

var packageDefinition = protoLoader.loadSync(
    'SampleData.proto',
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true
    });
var protoDescriptor = grpc.loadPackageDefinition(packageDefinition);
// The protoDescriptor object has the full package hierarchy
var sampledataservice = protoDescriptor.SampleData;


const server = new grpc.Server();

function GetSampleData(call, callback) {
    setTimeout(() => {
        callback(null, {
            id: 1,
            name: 'Abhinav Dhasmana',
            enjoys_coding: true,
        });
    }, 10);
}


server.addService(sampledataservice.SampleDataService.service, {
    GetSampleData: GetSampleData
})

server.bind('127.0.0.1:3003', grpc.ServerCredentials.createInsecure())
console.log('Server running at http://127.0.0.1:3003')
server.start()
console.log('grpc server is running');

app.get('/', async function (req, res) {
    await setTimeout(async () => {
        await res.send({
            id: 1,
            name: 'Abhinav Dhasmana',
            enjoys_coding: true,

        });
    }, 10);
});

app.listen(3002, function () {
    console.log('Service B listenig on port 3002');
});
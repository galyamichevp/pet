const http = require('http'); // load http interaction module
const fs = require('fs'); // load file system interaction module
const path = require('path'); // load dir interaction module
const url = require('url'); // load url resolution/parsing interaction module

const httpServer = require('./http.server')(5000);

function main() {
    console.log("main. enter.")

    var x = httpServer;

    console.log(`server state is`);

    console.log("main. exit.")
}

main();
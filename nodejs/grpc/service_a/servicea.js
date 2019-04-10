var express = require('express');
var app = express();
const axios = require('axios');

var grpc = require('grpc');
var protoLoader = require('@grpc/proto-loader');
var packageDefinition = protoLoader.loadSync(
    'SampleData.proto',
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true
    });
var sampledata = grpc.loadPackageDefinition(packageDefinition).SampleData;
var client = new sampledata.SampleDataService('127.0.0.1:3003', grpc.credentials.createInsecure());


app.get('/http', async function (req, res) {
    try {
        const response = await axios({
            url: 'http://localhost:3002/',
            method: 'GET',
        });
        return res.send(response.data);
    } catch (err) {
        throw console.log(err);
    }
});

app.get('/grpc', async function (req, res) {
    try {
        client.GetSampleData({}, (error, feature) => {
            return res.send(feature);
        });
    } catch (err) {
        throw console.log(err);
    }
});

app.listen(3001, function () {
    console.log('Service A listening on port 3001!');
});
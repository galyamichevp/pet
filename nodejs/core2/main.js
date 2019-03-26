const singletone = require('./app/singleton');

//singletone.t();
//singletone.x();
var x1 = singletone.z.getInstance();
var x2 = singletone.z.getInstance();

console.log(`equsls ? ${x1 == x2}`)

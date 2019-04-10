"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const linearSuperbSearch_1 = require("./solutions/linearSuperbSearch");
class Main {
    constructor() {
    }
    Run(method) {
        console.log("Starting...");
        method.Run();
    }
}
const entrypoint = new Main();
// entrypoint.Run(new BrutForce());
// entrypoint.Run(new BinarySearch());
// entrypoint.Run(new LinearSearch());
entrypoint.Run(new linearSuperbSearch_1.LinearSuperbSearch());

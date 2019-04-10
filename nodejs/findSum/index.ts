import { BrutForce } from './solutions/brutForce';
import { BinarySearch } from './solutions/binarySearch';
import { LinearSearch } from './solutions/linearSearch';
import { LinearSuperbSearch } from './solutions/linearSuperbSearch';
import { IRunning } from './solutions/interfaces';


class Main {
    constructor() {

    }

    Run(method: IRunning): void {
        console.log("Starting...");

        method.Run();
    }
}

const entrypoint = new Main();

// entrypoint.Run(new BrutForce());
// entrypoint.Run(new BinarySearch());
// entrypoint.Run(new LinearSearch());
entrypoint.Run(new LinearSuperbSearch());
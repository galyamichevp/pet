import { IRunning } from './interfaces';

// Time Complexity: O(N²)
// Space Complexity: O(1)
class BrutForce implements IRunning {

    FindSum(arr: number[], val: number): boolean {

        for (let i = 0; i < arr.length; i++) {
            for (let j = 0; j < arr.length; j++) {

                if (i !== j && arr[i] + arr[j] === val)
                    return true;
            }
        }

        return false;
    }

    Run(): void {
        var res1 = this.FindSum([1, 2, 4, 9], 8);
        console.log(`Res1 = ${res1}`);//false

        var res2 = this.FindSum([1, 2, 4, 4], 8);
        console.log(`Res2 = ${res2}`);//true
    }

}

export { BrutForce }
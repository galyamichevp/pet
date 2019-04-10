import { IRunning } from './interfaces';

// Time Complexity: O(N)
// Space Complexity: O(1)
class LinearSearch implements IRunning {

    FindSum(arr: number[], val: number): boolean {
        let start = 0;
        let end = arr.length - 1;

        while (start < end) {
            let sum = arr[start] + arr[end];

            if (sum > val) {
                end--;
            }
            else if (sum < val) {
                start++;
            }
            else {
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

export { LinearSearch }
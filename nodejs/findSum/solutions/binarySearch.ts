import { IRunning } from './interfaces';

// Time Complexity: O(Nlog(N))
// Space Complexity: O(N)
//!!! allowed for ordered array only
class BinarySearch implements IRunning {

    RemoveIndex(arr: number[], i: number): number[] {
        return arr.slice(0, i).concat(arr.slice(i + 1, arr.length));
    }

    Search(arr: number[], val: number): boolean {

        let start = 0;
        let end = arr.length - 1;
        let pivot = Math.floor(arr.length / 2);

        while (start < end) {
            if (val < arr[pivot]) {
                end = pivot - 1;
            }
            else if (val > arr[pivot]) {
                start = pivot + 1;
            }

            pivot = Math.floor((start + end) / 2);

            if (arr[pivot] === val) {
                return true;
            }
        }

        return false;
    }

    FindSum(arr: number[], val: number): boolean {

        for (let i = 0; i < arr.length; i++) {
            if (this.Search(this.RemoveIndex(arr, i), val - arr[i])) {
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

export { BinarySearch }
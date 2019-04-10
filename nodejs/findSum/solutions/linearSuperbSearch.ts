import { IRunning } from './interfaces';

// Time Complexity: O(N)
// Space Complexity: O(N)
// allowed to unordered array
class LinearSuperbSearch implements IRunning {

    FindSum(arr: number[], val: number): boolean {
        let searchValues = new Set();
        searchValues.add(val - arr[0]);

        for (let i = 1; i < arr.length; i++) {
            let sVal = val - arr[i];
            if (searchValues.has(sVal)) {
                return true;
            }
            else {
                searchValues.add(sVal);
            }
        }

        return false;
    }

    FindSumFit = (arr: number[], sum: number): boolean =>
        arr.some((set => (n: number) => set.has(n) || !set.add(sum - n))(new Set));

    Run(): void {
        var res1 = this.FindSum([1, 2, 4, 9], 8);
        console.log(`Res1 = ${res1}`);//false

        var res2 = this.FindSum([1, 2, 4, 4], 8);
        console.log(`Res2 = ${res2}`);//true


        var res1 = this.FindSumFit([1, 2, 4, 9], 8);
        console.log(`Res1 = ${res1}`);//false

        var res2 = this.FindSumFit([1, 2, 4, 4], 8);
        console.log(`Res2 = ${res2}`);//true
    }

}

export { LinearSuperbSearch }
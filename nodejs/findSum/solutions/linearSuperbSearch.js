"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// Time Complexity: O(N)
// Space Complexity: O(N)
// allowed to unordered array
class LinearSuperbSearch {
    constructor() {
        this.FindSumFit = (arr, sum) => arr.some((set => (n) => set.has(n) || !set.add(sum - n))(new Set));
    }
    FindSum(arr, val) {
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
    Run() {
        var res1 = this.FindSum([1, 2, 4, 9], 8);
        console.log(`Res1 = ${res1}`); //false
        var res2 = this.FindSum([1, 2, 4, 4], 8);
        console.log(`Res2 = ${res2}`); //true
        var res1 = this.FindSumFit([1, 2, 4, 9], 8);
        console.log(`Res1 = ${res1}`); //false
        var res2 = this.FindSumFit([1, 2, 4, 4], 8);
        console.log(`Res2 = ${res2}`); //true
    }
}
exports.LinearSuperbSearch = LinearSuperbSearch;

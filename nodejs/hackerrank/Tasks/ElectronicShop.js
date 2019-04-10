//PracticeAlgorithmsImplementationElectronics Shop

function ElectronicShop() {
    console.log('ElectronicShop.in===>');

    var result = -1;
    var keyboards = [3, 1];
    var drives = [5, 2, 8];
    var b = 10;


    var arr1 = keyboards
        .filter((x, i) => keyboards.indexOf(x) === i)
        .reverse();
    var arr2 = drives
        .filter((x, i) => drives.indexOf(x) === i)
        .sort();

    for (let i = 0; i < arr1.length; i++) {
        var j = 0;
        for (j; j < arr2.length; j++) {
            if (arr1[i] + arr2[j] > b) break;
            if (arr1[i] + arr2[j] > result) result = arr1[i] + arr2[j];
        }
    }



    console.log(`Result=${result}`);

    console.log('ElectronicShop.out===>');
}

module.exports = { ElectronicShop };
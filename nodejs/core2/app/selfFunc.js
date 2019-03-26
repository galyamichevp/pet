const myModule = (function () {
    const pVariable = 'hello world';

    function pMethod() {
        console.log(pVariable);
    }

    return {
        xMethod: function () { pMethod(); }
    }
})();

module.exports = myModule;
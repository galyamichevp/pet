const sm = require('./Tasks/SockMerchant');
const vc = require('./Tasks/ValleyCount');
const jc = require('./Tasks/JumpingCloud');
const es = require('./Tasks/ElectronicShop');


const main = (function () {
    console.log('runnig');

    // sm.SockMerchant();
    // vc.ValleyCount();
    // jc.JampingCloud();
    es.ElectronicShop();

    console.log('completd');
})();
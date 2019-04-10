//PracticeInterview Preparation KitWarm-up ChallengesSock Merchant
function SockMerchant() {
    console.log('SockMerchant.in===>');

    var ar = [10, 20, 20, 10, 10, 30, 50, 10, 20];

    var res = ar
        .reduce((s, val) => {
            s[val] = (s[val] + 1) || 1;
            console.log(`reduce val=${val}; s[val]=${s[val]}`);
            return s;
        }, [])
        // .filter(v => v)
        .reduce((r, v) => r + (v / 2 | 0), 0);

    console.log(`res length=${res}`);

    console.log('SockMerchant.out===>');
}

module.exports = { SockMerchant };
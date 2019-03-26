let instance = null;

function User() {
    if (instance) {
        return instance;
    }
    instance = this;
    this.name = 'Peter';
    this.age = 25;

    return instance;
}

function tSingletone() {
    const user = {
        name: 'Peter',
        age: 25,
        job: 'Teacher',
        greet: function () {
            console.log('Hello!');
        }
    };

    const user1 = user;
    user1.name = 'Mark';

    // prints 'Mark'
    console.log(user.name);
    // prints 'Mark'
    console.log(user1.name);
    // prints true
    console.log(user === user1);
}

function xSingletone() {
    const user1 = new User();
    const user2 = new User();
    // prints true
    console.log(user1 === user2);
}

const zSingletone = (function () {
    let instance;

    function init() {
        return {
            name: 'Peter',
            age: 23
        };
    }

    return {
        getInstance: function () {
            if (!instance) instance = init();

            return instance;
        }
    }
})();


module.exports.t = tSingletone;
module.exports.x = xSingletone;
module.exports.z = zSingletone;
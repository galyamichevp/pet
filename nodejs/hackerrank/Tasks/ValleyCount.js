//PracticeInterview Preparation KitWarm-up ChallengesCounting Valleys


function ValleyCount() {
    console.log('ValleyCount.in===>');

    var ar = ['U', 'D', 'D', 'D', 'U', 'D', 'U', 'U'];

    var valleyCnt = 0;
    var level = 0;

    ar.map(v => {
        if (v === 'D' && level === 0) {
            valleyCnt++;
            level--;

            console.log(`p1: valleyCnt=${valleyCnt}; level=${level}`);
        }
        else if (v === 'D') {
            level--;

            console.log(`p2: valleyCnt=${valleyCnt}; level=${level}`);
        }
        else if (v === 'U') {
            level++;

            console.log(`p3: valleyCnt=${valleyCnt}; level=${level}`);
        }
    });

    console.log(`res length=${valleyCnt}`);

    console.log('ValleyCount.out===>');
}

module.exports = { ValleyCount };
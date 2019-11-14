//const max_fireworks = 5;
//const max_sparks = 50;
//const fireworks = [];

//function createFireworks() {
//    for (let i = 0; i < max_fireworks; i++) {
//        let firework = {
//            sparks: []
//        };

//        for (let n = 0; n < max_sparks; n++) {
//            let spark = {
//                vx: Math.random() * 5 + .5,
//                vy: Math.random() * 5 + .5,
//                weight: Math.random() * .3 + .03,
//                red: Math.floor(Math.random() * 2),
//                green: Math.floor(Math.random() * 2),
//                blue: Math.floor(Math.random() * 2)
//            };

//            if (Math.random() > .5) {
//                spark.vx = -spark.vx;
//            }

//            if (Math.random() > .5) {
//                spark.vy = -spark.vy;
//            }

//            firework.sparks.push(spark);
//        }

//        fireworks.push(firework);
//        resetFirework(firework);
//    }
//}

//function resetFirework(firework) {
//    firework.x = Math.floor(Math.random() * canvas.width);
//    firework.y = canvas.height;
//    firework.age = 0;
//    firework.phase = 'fly';
//}

const canvas = document.getElementById('myCanvas');
const context = canvas.getContext('2d');
const fireworks = [
    new Firework(canvas, context).setup(),
    new Firework(canvas, context).setup(),
    new Firework(canvas, context).setup(),
    new Firework(canvas, context).setup(),
    new Firework(canvas, context).setup()
];

function showFireworks() {
    context.clearRect(0, 0, canvas.width, canvas.height);

    fireworks.forEach((firework, index) => {
        firework.explode();
    });

    window.requestAnimationFrame(showFireworks);
}

canvas.height = $(document).height();
canvas.width = $(document).width();


function Firework(canvasIn, contextIn) {
    const canvas = canvasIn;
    const context = contextIn;

    this.phase = 'fly';
    this.age = 0;
    this.x = Math.floor(Math.random() * canvas.width);
    this.y = canvas.height;
    this.sparks = [];

    this.setup = function () {
        this.createSparks();

        return this;
    }

    this.createSparks = function () {
        const sparksAmount = Math.random() * 100;

        for (let i = 0; i <= sparksAmount; i++) {
            const spark = {
                vx: Math.random() * 5 + .5,
                vy: Math.random() * 5 + .5,
                weight: Math.random() * .3 + .03,
                red: Math.floor(Math.random() * 2),
                green: Math.floor(Math.random() * 2),
                blue: Math.floor(Math.random() * 2)
            };

            if (Math.random() > .5) {
                spark.vx = -spark.vx;
            }

            if (Math.random() > .5) {
                spark.vy = -spark.vy;
            }

            this.sparks.push(spark);
        }
    };

    this.explode = function () {
        if (this.phase = 'explode') {
             this.sparks.forEach((spark) => {
                for (let i = 0; i < 10; i++) {
                    const trailAge = this.age + i;
                    const x = this.x + spark.vx * trailAge;
                    const y = this.y + spark.vy * trailAge + spark.weight * trailAge * spark.weight * trailAge;
                    const fade = i * 20 - this.age * 2;
                    const r = Math.floor(spark.red * fade);
                    const g = Math.floor(spark.green * fade);
                    const b = Math.floor(spark.blue * fade);

                    context.beginPath();
                    context.fillStyle = 'rgba(' + r + ',' + g + ',' + b + ',1)';
                    context.rect(x, y, 4, 4);
                    context.fill();
                 }
            });

            this.age++;

            if (this.age > 100 && Math.random() < .05) {
                this.reset();
            }
        } else {
            this.y = this.y - 10;

            for (let spark = 0; spark < 15; spark++) {
                context.beginPath();
                context.fillStyle = 'rgba(' + index * 50 + ',' + spark * 17 + ',0,1)';
                context.rect(this.x + Math.random() * spark - spark / 2, this.y + spark * 4, 4, 4);
                context.fill();
            }

            if (Math.random() < .001 || this.y < 200) {
                this.phase = 'explode'
            }
        }
    };

    this.reset = function () {
        this.x = Math.floor(Math.random() * canvas.width);
        this.y = canvas.height;
        this.age = 0;
        this.phase = 'fly';
    };
}
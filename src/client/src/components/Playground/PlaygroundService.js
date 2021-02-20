import Matter from 'matter-js'

export default class PlaygroundService {
    constructor(playgroundViewRef, canvasRef, width, height) {
        this.playgroundViewRef = playgroundViewRef;
        this.canvasRef = canvasRef;

        this.playgroundWidth = width;
        this.playgroundHeight = height;

        this.green = '#00ff00';
        this.red = '#ff0000';
        this.yellow = '#ffff00';
    }

    initialize() {
        var runner = Matter.Runner.create();

        var engine = Matter.Engine.create({
            world: Matter.World.create({ gravity: { x: 0, y: 0 } })
        });

        var render = Matter.Render.create({
            element: this.playgroundViewRef.current,
            canvas: this.canvasRef.current,
            engine: engine,
            options: {
                width: this.playgroundWidth,
                height: this.playgroundHeight,
                wireframes: false
            }
        });

        var car = this.createActor(150, 100);
        this.createMap(engine.world);


        Matter.World.add(engine.world, car);

        Matter.Events.on(engine, 'collisionStart', event => {
            var pairs = event.pairs;

            for (var i = 0, j = pairs.length; i != j; ++i) {
                var pair = pairs[i];

                // if (pair.bodyA === collider) {
                pair.bodyB.render.fillStyle = this.red;
                // } else if (pair.bodyB === collider) {
                pair.bodyA.render.fillStyle = this.red;
                // }
            }
        });

        Matter.Events.on(engine, 'collisionEnd', event => {
            var pairs = event.pairs;

            for (var i = 0, j = pairs.length; i != j; ++i) {
                var pair = pairs[i];

                // if (pair.bodyA === collider) {
                pair.bodyB.render.fillStyle = this.green;
                // } else if (pair.bodyB === collider) {
                pair.bodyA.render.fillStyle = this.green;
                // }
            }
        });

        var keys = [];

        document.body.addEventListener("keydown", function (e) {
            keys[e.keyCode] = true;
        });
        document.body.addEventListener("keyup", function (e) {
            keys[e.keyCode] = false;
        });

        Matter.Events.on(engine, "beforeTick", function (event) {
            if (keys[38]) {
                Matter.Composite.translate(car, Matter.Vector.create(Math.cos(car.bodies[0].angle), Math.sin(car.bodies[0].angle)));

            }
            if (keys[37]) {
                Matter.Composite.rotate(car, -Math.sin(0.01), {
                    x: car.bodies[0].position.x,
                    y: car.bodies[0].position.y
                });
            }
            if (keys[39]) {
                Matter.Composite.rotate(car, Math.sin(0.01), {
                    x: car.bodies[0].position.x,
                    y: car.bodies[0].position.y
                });

            }
            if (keys[40]) {
                Matter.Composite.translate(car, Matter.Vector.create(-Math.cos(car.bodies[0].angle), -Math.sin(car.bodies[0].angle)));
                // Matter.Body.setVelocity(car, Matter.Vector.create(-Math.cos(car.bodies[0].angle), -Math.sin(car.bodies[0].angle)));
            }
        });

        Matter.Render.lookAt(render, {
            min: { x: 0, y: 0 },
            max: { x: this.playgroundWidth, y: this.playgroundHeight }
        });

        Matter.Runner.run(runner, engine);
        Matter.Engine.run(engine);
        Matter.Render.run(render);
    }

    createActor(x, y) {
        var actorSize = 20;
        var sensorWidth = 100;
        var sensorHeight = 5;

        var group = Matter.Body.nextGroup(true);

        var actor = Matter.Composite.create({ label: 'Actor' });

        var body = Matter.Bodies.circle(x, y, actorSize, {
            collisionFilter: {
                group: group
            },
            // isStatic: true,
            isSensor: true,
            render: {
                fillStyle: this.green,
                lineWidth: 0
            }
        });

        var sensor1 = Matter.Bodies.rectangle(x + actorSize + sensorWidth / 2, y, sensorWidth, sensorHeight, {
            collisionFilter: {
                group: group
            },
            // isStatic: true,
            isSensor: true,
            render: {
                fillStyle: this.green,
                lineWidth: 0
            }
        });

        var sensor2 = Matter.Bodies.rectangle(x, y + actorSize + sensorWidth / 2, sensorHeight, sensorWidth, {
            collisionFilter: {
                group: group
            },
            // isStatic: true,
            isSensor: true,
            render: {
                fillStyle: this.green,
                lineWidth: 0
            }
        });

        var sensor3 = Matter.Bodies.rectangle(x, y - (actorSize + sensorWidth / 2), sensorHeight, sensorWidth, {
            collisionFilter: {
                group: group
            },
            // isStatic: true,
            isSensor: true,
            render: {
                fillStyle: this.green,
                lineWidth: 0
            }
        });

        var sensor4 = Matter.Bodies.rectangle(x + sensorWidth / 2, y + sensorWidth / 2, sensorWidth, sensorHeight, {
            collisionFilter: {
                group: group
            },
            // isStatic: true,
            isSensor: true,
            render: {
                fillStyle: this.green,
                lineWidth: 0
            },
            angle: 0.7
        });

        var sensor5 = Matter.Bodies.rectangle(x + sensorWidth / 2, y - sensorWidth / 2, sensorWidth, sensorHeight, {
            collisionFilter: {
                group: group
            },
            // isStatic: true,
            isSensor: true,
            render: {
                fillStyle: this.green,
                lineWidth: 0
            },
            angle: -0.7
        });

        Matter.Composite.addBody(actor, body);
        Matter.Composite.addBody(actor, sensor1);
        Matter.Composite.addBody(actor, sensor2);
        Matter.Composite.addBody(actor, sensor3);

        Matter.Composite.addBody(actor, sensor4);
        Matter.Composite.addBody(actor, sensor5);

        return actor;
    };

    createMap(world) {
        Matter.World.add(world, [
            this.placeWall(50, 10, 1400, 20),
            this.placeWall(200, 150, 1300, 160),

            this.placeWall(1400, 10, 1410, 800),
            this.placeWall(1300, 150, 1310, 700),

            this.placeWall(1000, 700, 1310, 710),
            this.placeWall(850, 800, 1410, 810),

            this.placeWall(850, 400, 860, 800),
            this.placeWall(1000, 250, 1010, 700),

            this.placeWall(500, 250, 1010, 260),
            this.placeWall(650, 400, 860, 410),

            this.placeWall(650, 400, 660, 800),
            this.placeWall(500, 250, 510, 700),

            this.placeWall(200, 700, 510, 710),
            this.placeWall(50, 800, 660, 810),

            this.placeWall(50, 10, 60, 800),
            this.placeWall(200, 150, 210, 700),
        ]);
    };

    placeWall(x1, y1, x2, y2) {
        var vertices = [
            { x: x1, y: y1 },
            { x: x1, y: y2 },
            { x: x2, y: y2 },
            { x: x2, y: y1 }
        ]

        var body = Matter.Body.create({
            position: Matter.Vertices.centre(vertices),
            vertices: vertices,
            isStatic: true,
            render: {
                fillStyle: this.yellow,
                lineWidth: 0
            },
        });
        return body;
    }
}
import Phaser from 'phaser'
import * as signalR from "@microsoft/signalr"
import Sensor from './Sensor';
import Actor from './Actor';

export default class PlaygroundService {
    constructor(playgroundViewRef, canvasRef, width, height) {
        this.playgroundViewRef = playgroundViewRef;
        this.canvasRef = canvasRef;

        this.playgroundWidth = width;
        this.playgroundHeight = height;
    }

    initialize() {
        var green = 0x00ff00;
        var red = 0xff0000;
        var yellow = "#ffff00";

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:5001/hub")
            .build()

        connection.on("ReceiveAngle", (id, angle) => {
            actor.angle = angle
            actor.translateActor(new Phaser.Math.Vector2(1, 1));
        });

        function angle(id, sensors) {
            connection
                .invoke("GetAngle", id, sensors)
                .catch(err => console.log(err.toString()));
        }

        function evaluate(id, evalua) {
            connection
                .invoke("Evaluate", id, evalua)
                .catch(err => console.log(err.toString()));
        }

        connection
            .start()
            .then(() => console.log("Sudoku component connection started."))
            .catch(error => console.log(error))

        var graphics;

        var actor = new Actor(225, 80, 0, new Phaser.Geom.Circle(null, null, 10), [
            new Sensor("Sensor 1", -90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            new Sensor("Sensor 2", -45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            new Sensor("Sensor 3", -15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            new Sensor("Sensor 4", 15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            new Sensor("Sensor 5", 45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            new Sensor("Sensor 6", 90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
        ]);

        var text;
        var walls = [
            new Phaser.Geom.Line(200, 10, 200, 150),
            new Phaser.Geom.Line(200, 10, 1400, 10),
            new Phaser.Geom.Line(200, 150, 1300, 150),
            new Phaser.Geom.Line(1400, 10, 1400, 800),
            new Phaser.Geom.Line(1300, 150, 1300, 700),
            new Phaser.Geom.Line(1000, 700, 1300, 700),
            new Phaser.Geom.Line(850, 800, 1400, 800),
            new Phaser.Geom.Line(850, 400, 850, 800),
            new Phaser.Geom.Line(1000, 250, 1000, 700),
            new Phaser.Geom.Line(500, 250, 1000, 250),
            new Phaser.Geom.Line(650, 400, 850, 400),
            new Phaser.Geom.Line(650, 400, 650, 800),
            new Phaser.Geom.Line(500, 250, 500, 700),
            new Phaser.Geom.Line(150, 700, 500, 700),
            new Phaser.Geom.Line(10, 800, 650, 800),
            new Phaser.Geom.Line(10, 10, 10, 800),
            new Phaser.Geom.Line(150, 10, 150, 700),
            new Phaser.Geom.Line(10, 10, 150, 10),
        ];

        var checkpoints = [
            new Phaser.Geom.Line(300, 20, 300, 140),
            new Phaser.Geom.Line(400, 20, 400, 140),
            new Phaser.Geom.Line(500, 20, 500, 140),
            new Phaser.Geom.Line(600, 20, 600, 140),
            new Phaser.Geom.Line(700, 20, 700, 140),
            new Phaser.Geom.Line(800, 20, 800, 140),
            new Phaser.Geom.Line(900, 20, 900, 140),
            new Phaser.Geom.Line(1000, 20, 1000, 140),
            new Phaser.Geom.Line(1100, 20, 1100, 140),
            new Phaser.Geom.Line(1200, 20, 1200, 140),
            new Phaser.Geom.Line(1300, 20, 1300, 140),

            new Phaser.Geom.Line(1310, 150, 1390, 150),
            new Phaser.Geom.Line(1310, 250, 1390, 250),
            new Phaser.Geom.Line(1310, 350, 1390, 350),
            new Phaser.Geom.Line(1310, 450, 1390, 450),
            new Phaser.Geom.Line(1310, 550, 1390, 550),
            new Phaser.Geom.Line(1310, 650, 1390, 650),

            new Phaser.Geom.Line(1300, 710, 1300, 790),
            new Phaser.Geom.Line(1200, 710, 1200, 790),
            new Phaser.Geom.Line(1100, 710, 1100, 790),
            new Phaser.Geom.Line(1000, 710, 1000, 790),

            new Phaser.Geom.Line(860, 700, 990, 700),
            new Phaser.Geom.Line(860, 600, 990, 600),
            new Phaser.Geom.Line(860, 500, 990, 500),
            new Phaser.Geom.Line(860, 400, 990, 400),

            new Phaser.Geom.Line(850, 260, 850, 390),
            new Phaser.Geom.Line(750, 260, 750, 390),
            new Phaser.Geom.Line(650, 260, 650, 390),

            new Phaser.Geom.Line(510, 400, 640, 400),
            new Phaser.Geom.Line(510, 500, 640, 500),
            new Phaser.Geom.Line(510, 600, 640, 600),
            new Phaser.Geom.Line(510, 700, 640, 700),

            new Phaser.Geom.Line(500, 710, 500, 790),
            new Phaser.Geom.Line(400, 710, 400, 790),
            new Phaser.Geom.Line(300, 710, 300, 790),
            new Phaser.Geom.Line(200, 710, 200, 790),

            new Phaser.Geom.Line(20, 700, 140, 700),
            new Phaser.Geom.Line(20, 600, 140, 600),
            new Phaser.Geom.Line(20, 500, 140, 500),
            new Phaser.Geom.Line(20, 400, 140, 400),
            new Phaser.Geom.Line(20, 300, 140, 300),
            new Phaser.Geom.Line(20, 200, 140, 200),
            new Phaser.Geom.Line(20, 100, 140, 100),
        ];

        var keyUp;
        var keyDown;
        var keyLeft;
        var keyRight;

        var game = new Phaser.Game({
            width: this.playgroundWidth,
            height: this.playgroundHeight,
            type: Phaser.AUTO,
            parent: 'playground',
            scene: {
                create: create,
                update: update
            }
        });

        function create() {
            graphics = this.add.graphics();
            createKeyboardInputs(this.input);

            text = this.add.text(10, 810, '', { font: '16px Courier', fill: yellow });
        }

        function update() {
            graphics.clear();
            graphics.lineStyle(2, green);

            walls.forEach(wall => {
                graphics.strokeLineShape(wall);
            });

            graphics.lineStyle(1, 0xffff00);
            checkpoints.forEach(checkpoint => {
                graphics.strokeLineShape(checkpoint);
            });

            drawActor();

            text.setText(
                `Checkpoints: ${count.filter(Boolean).length}(${count.filter(Boolean).length/count.length})|Alive:${actor.isAlive}\n` +
                actor.sensors.map(sensor => `${sensor.toString()}`).join("\n"));

            hangleKeyboardInputs();
            countCheckpoints()
        }

        var count = new Array(checkpoints.length).fill(false);

        function countCheckpoints() {
            for (let index = 0; index < checkpoints.length; index++) {
                const checkpoint = checkpoints[index];

                if (Phaser.Geom.Intersects.LineToCircle(checkpoint, actor.actorObject)) {
                    count[index] = true;
                    break;
                }
            }
        }

        function hangleKeyboardInputs() {
            angle("1", actor.sensors.map(sensor => sensor.collisionCoordinate.distance ?? 1.0));
            actor.setSensorsPosition();

            if (keyUp.isDown) {
                var translationVector = new Phaser.Math.Vector2(1, 1);
                actor.translateActor(translationVector);
            }
            if (keyDown.isDown) {
                var translationVector = new Phaser.Math.Vector2(-1, 1);
                actor.translateActor(translationVector);
            }
            if (keyLeft.isDown) {
                actor.rotate(actor.angle - 1);
            }
            if (keyRight.isDown) {
                actor.rotate(actor.angle + 1);
            }
        }

        function drawActor() {
            graphics.lineStyle(2, red);
            graphics.strokeCircleShape(actor.actorObject);
            actor.sensors.forEach(sensor => {
                graphics.strokeLineShape(sensor.sensorLine);
            });
            drawCollisionPoints();
        }

        function drawCollisionPoints() {
            for (let index = 0; index < actor.sensors.length; index++) {
                const sensor = actor.sensors[index];

                for (let index = 0; index < walls.length; index++) {
                    const wall = walls[index];

                    if(actor.isAlive && Phaser.Geom.Intersects.LineToCircle(wall, actor.actorObject))
                    {
                        actor.isAlive = false;
                    }

                    if (Phaser.Geom.Intersects.LineToLine(sensor.sensorLine, wall)) {
                        var intersection = Phaser.Geom.Intersects.GetLineToLine(sensor.sensorLine, wall);
                        sensor.setCollision(intersection?.x, intersection?.y, intersection?.z)
                        graphics.strokeCircleShape(sensor.collisionCircle);
                        break;
                    }
                    else sensor.reset()
                }
            }
        }

        function createKeyboardInputs(input) {
            keyUp = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.UP);
            keyDown = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.DOWN);
            keyLeft = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.LEFT);
            keyRight = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.RIGHT);
        }
    }
}
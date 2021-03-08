import Phaser from 'phaser'
import * as signalR from "@microsoft/signalr"
import Sensor from './Sensor';

export default class PlaygroundService {
    constructor(playgroundViewRef, canvasRef, width, height) {
        this.playgroundViewRef = playgroundViewRef;
        this.canvasRef = canvasRef;

        this.playgroundWidth = width;
        this.playgroundHeight = height;
    }

    initialize() {
        var connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:5001/hub")
            .build()

        connection.on("ReceiveAngle", (id, angle) => degree = angle)

        connection
            .start()
            .then(() => console.log("Sudoku component connection started."))
            .catch(error => console.log(error))

        var green = 0x00ff00;
        var red = 0xff0000;
        var yellow = 0xffff00;

        var x = 225;
        var y = 80;
        var degree = 0;

        var graphics;

        var actor;

        var sensors = []

        var text;

        var walls;

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
            createActor();
            createMap();

            text = this.add.text(10, 700,
                '',
                { font: '16px Courier', fill: '#ffff00' });
        }

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

        function update() {
            graphics.clear();
            graphics.lineStyle(2, green);

            walls.forEach(wall => {
                graphics.strokeLineShape(wall);
            });

            drawActor();

            text.setText(sensors.map(sensor => `${sensor.toString()}`).join("\n"));

            hangleKeyboardInputs();
        }

        function drawCollisionPoints() {
            for (let index = 0; index < sensors.length; index++) {
                const sensor = sensors[index];

                for (let index = 0; index < walls.length; index++) {
                    const wall = walls[index];

                    if (Phaser.Geom.Intersects.LineToLine(sensor.sensorLine, wall)) {
                        var intersection = Phaser.Geom.Intersects.GetLineToLine(sensor.sensorLine, wall);
                        sensor.setCollision(intersection?.x, intersection?.y, intersection?.z)
                        graphics.strokeCircleShape(sensor.collisionCircle);
                        break;
                    }
                }
            }
        }

        function createKeyboardInputs(input) {
            keyUp = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.UP);
            keyDown = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.DOWN);
            keyLeft = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.LEFT);
            keyRight = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.RIGHT);
        }

        function createActor() {
            actor = new Phaser.Geom.Circle(x, y, 10);
            sensors = [
                new Sensor("Sensor 1", -90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 2", -45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 3", -15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 4", 15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 5", 45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 6", 90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            ];

            setSensorsPosition();
        }

        function drawActor() {
            graphics.lineStyle(2, red);
            graphics.strokeCircleShape(actor);
            sensors.forEach(sensor => {
                graphics.strokeLineShape(sensor.sensorLine);
            });
            drawCollisionPoints();
        }

        function createMap() {
            walls = [
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
            ]
        }

        var getAngleWithOffset = (offset) => (offset + degree) * Math.PI / 180;

        function translateActor(translationVector) {
            translationVector.setAngle(getAngleWithOffset(0));
            Phaser.Geom.Circle.Offset(actor, translationVector.x, translationVector.y);

            sensors.forEach(sensor => {
                Phaser.Geom.Line.Offset(sensor.sensorLine, translationVector.x, translationVector.y);
            });

            x = actor.x;
            y = actor.y;
        }

        function setSensorsPosition() {
            sensors.forEach(sensor => {
                Phaser.Geom.Line.SetToAngle(sensor.sensorLine, x, y, sensor.getAngle(degree), sensor.width);
            });
        }

        function hangleKeyboardInputs() {
            angle("1", sensors.map(sensor => sensor.collisionCoordinate.distance ?? 1.0));
            setSensorsPosition();

            if (keyUp.isDown) {
                var translationVector = new Phaser.Math.Vector2(1, 1);
                translateActor(translationVector);
            }
            if (keyDown.isDown) {
                var translationVector = new Phaser.Math.Vector2(-1, 1);
                translateActor(translationVector);
            }
            if (keyLeft.isDown) {
                degree = degree - 1;
                setSensorsPosition();
            }
            if (keyRight.isDown) {
                degree = degree + 1;
                setSensorsPosition();
            }
        }
    }
}
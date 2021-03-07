import Phaser from 'phaser'
import * as signalR from "@microsoft/signalr"

class CollisionCoordinates {
    constructor() {
        this.x = null;
        this.y = null;
        this.distance = null;
    }

    setCoordinates(x, y, distance) {
        this.x = x;
        this.y = y;
        this.distance = distance;
    }

    toString() {
        return this.x && this.y && this.distance
            ? `(${this.x}, ${this.y}):${this.distance}`
            : "Undefined";
    }
}

class Sensor {
    constructor(id, offset, sensorLine, collisionCircle) {
        this.id = id;
        this.defaultWidth = 100;
        this.width = this.defaultWidth;
        this.collisionCoordinates = new CollisionCoordinates();
        this.offset = offset;
        this.sensorLine = sensorLine;
        this.collisionCircle = collisionCircle;
    }

    getAngle(angle) {
        return (this.offset + angle) * Math.PI / 180;
    }

    setColision(x, y, distance) {
        if (x && y && distance) {
            this.collisionCoordinates.setCoordinates(x, y, distance);
            // this.width = this.defaultWidth * distance;
            this.collisionCircle.setPosition(this.collisionCoordinates.x, this.collisionCoordinates.y);
        }
    }

    isColiding() {
        return this.collisionCoordinates.x && this.collisionCoordinates.y && this.collisionCoordinates.distance
            ? true
            : false;
    }

    reset() {
        this.collisionCoordinates.x = null;
        this.collisionCoordinates.y = null;
        this.collisionCoordinates.distance = null;
    }

    toString() {
        return `${this.id}: ${this.collisionCoordinates.toString()}`;
    }
}

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

        var sensorWidth = 100;
        var x = 225;
        var y = 80;
        var degree = 0;

        var graphics;

        var actor;

        var sensor1 = new Sensor("Sensor 1", -90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10));
        var sensor2 = new Sensor("Sensor 2", -45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10));
        var sensor3 = new Sensor("Sensor 3", -15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10));
        var sensor4 = new Sensor("Sensor 4", 15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10));
        var sensor5 = new Sensor("Sensor 5", 45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10));
        var sensor6 = new Sensor("Sensor 6", 90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10));

        var sensors = [

        ]

        // var collision2;
        // var collision3;
        // var collision4;
        // var collision5;
        // var collision6;

        var text;

        var inte1
        var inte2
        var inte3
        var inte4
        var inte5
        var inte6

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
            createCollisionPoints();
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
            hangleKeyboardInputs();

            graphics.strokeCircleShape(actor);

            graphics.lineStyle(2, green);
            graphics.strokeLineShape(sensor1.sensorLine);
            graphics.strokeLineShape(sensor2.sensorLine);
            graphics.strokeLineShape(sensor3.sensorLine);
            graphics.strokeLineShape(sensor4.sensorLine);
            graphics.strokeLineShape(sensor5.sensorLine);
            graphics.strokeLineShape(sensor6.sensorLine);
            if (sensor1.isColiding()) graphics.strokeCircleShape(sensor1.collisionCircle);

            drawCollisionPoints();

            walls.forEach(wall => {
                graphics.strokeLineShape(wall);
            });

            text.setText(
                `${sensor1.toString()}\n` +
                `${sensor2.toString()}\n` +
                `${sensor3.toString()}\n` +
                `${sensor4.toString()}\n` +
                `${sensor5.toString()}\n` +
                `${sensor6.toString()}\n`);
        }

        function drawCollisionPoints() {
            inte1 = undefined;
            inte2 = undefined;
            inte3 = undefined;
            inte4 = undefined;
            inte5 = undefined;
            inte6 = undefined;

            // for (let index = 0; index < sensors.length; index++) {
            //     const sensor = sensors[index];

                for (let index = 0; index < walls.length; index++) {
                    const wall = walls[index];

                    if (Phaser.Geom.Intersects.LineToLine(sensor1.sensorLine, wall)) {
                        inte1 = Phaser.Geom.Intersects.GetLineToLine(sensor1.sensorLine, wall);
                        sensor1.setColision(inte1?.x, inte1?.y, inte1?.z)
                    };

                    if (Phaser.Geom.Intersects.LineToLine(sensor2.sensorLine, wall)) {
                        inte2 = Phaser.Geom.Intersects.GetLineToLine(sensor2.sensorLine, wall);
                        sensor2.setColision(inte2?.x, inte2?.y, inte2?.z)
                        graphics.strokeCircleShape(sensor2.collisionCircle);
                        
                    }

                    if (Phaser.Geom.Intersects.LineToLine(sensor3.sensorLine, wall)) {
                        inte3 = Phaser.Geom.Intersects.GetLineToLine(sensor3.sensorLine, wall);
                        sensor3.setColision(inte3?.x, inte3?.y, inte3?.z)

                        graphics.strokeCircleShape(sensor3.collisionCircle);
                    }

                    if (Phaser.Geom.Intersects.LineToLine(sensor4.sensorLine, wall)) {
                        inte4 = Phaser.Geom.Intersects.GetLineToLine(sensor4.sensorLine, wall);
                        sensor4.setColision(inte4?.x, inte4?.y, inte4?.z)

                        graphics.strokeCircleShape(sensor4.collisionCircle);
                    }

                    if (Phaser.Geom.Intersects.LineToLine(sensor5.sensorLine, wall)) {
                        inte5 = Phaser.Geom.Intersects.GetLineToLine(sensor5.sensorLine, wall);
                        sensor5.setColision(inte5?.x, inte5?.y, inte5?.z)
                        graphics.strokeCircleShape(sensor5.collisionCircle);
                    }

                    if (Phaser.Geom.Intersects.LineToLine(sensor6.sensorLine, wall)) {
                        inte6 = Phaser.Geom.Intersects.GetLineToLine(sensor6.sensorLine, wall);
                        sensor6.setColision(inte6?.x, inte6?.y, inte6?.z)
                        graphics.strokeCircleShape(sensor6.collisionCircle);
                    }
                }
            // }

        }

        function createKeyboardInputs(input) {
            keyUp = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.UP);
            keyDown = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.DOWN);
            keyLeft = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.LEFT);
            keyRight = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.RIGHT);
        }

        function createActor() {
            actor = new Phaser.Geom.Circle(x, y, 10);
            // sensor1 = new Phaser.Geom.Line();
            // sensor2 = new Phaser.Geom.Line();
            // sensor3 = new Phaser.Geom.Line();
            // sensor4 = new Phaser.Geom.Line();
            // sensor5 = new Phaser.Geom.Line();
            // sensor6 = new Phaser.Geom.Line();

            setSensorsPosition();
        }

        function createCollisionPoints() {
            // collision1 = new Phaser.Geom.Circle(null, null, 10);
            // collision2 = new Phaser.Geom.Circle(null, null, 10);
            // collision3 = new Phaser.Geom.Circle(null, null, 10);
            // collision4 = new Phaser.Geom.Circle(null, null, 10);
            // collision5 = new Phaser.Geom.Circle(null, null, 10);
            // collision6 = new Phaser.Geom.Circle(null, null, 10);
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
            Phaser.Geom.Line.Offset(sensor1.sensorLine, translationVector.x, translationVector.y);
            Phaser.Geom.Line.Offset(sensor2.sensorLine, translationVector.x, translationVector.y);
            Phaser.Geom.Line.Offset(sensor3.sensorLine, translationVector.x, translationVector.y);
            Phaser.Geom.Line.Offset(sensor4.sensorLine, translationVector.x, translationVector.y);
            Phaser.Geom.Line.Offset(sensor5.sensorLine, translationVector.x, translationVector.y);
            Phaser.Geom.Line.Offset(sensor6.sensorLine, translationVector.x, translationVector.y);

            x = actor.x;
            y = actor.y;
        }

        function setSensorsPosition() {
            Phaser.Geom.Line.SetToAngle(sensor1.sensorLine, x, y, sensor1.getAngle(degree), sensor1.width);
            Phaser.Geom.Line.SetToAngle(sensor2.sensorLine, x, y, sensor2.getAngle(degree), sensor2.width);
            Phaser.Geom.Line.SetToAngle(sensor3.sensorLine, x, y, sensor3.getAngle(degree), sensor3.width);
            Phaser.Geom.Line.SetToAngle(sensor4.sensorLine, x, y, sensor4.getAngle(degree), sensor4.width);
            Phaser.Geom.Line.SetToAngle(sensor5.sensorLine, x, y, sensor5.getAngle(degree), sensor5.width);
            Phaser.Geom.Line.SetToAngle(sensor6.sensorLine, x, y, sensor6.getAngle(degree), sensor6.width);
        }

        function hangleKeyboardInputs() {
            angle("1", [inte1?.z ?? 1.0, inte2?.z ?? 1.0, inte3?.z ?? 1.0, inte4?.z ?? 1.0, inte5?.z ?? 1.0, inte6?.z ?? 1.0]);
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
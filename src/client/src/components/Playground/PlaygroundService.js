import Phaser from 'phaser'
import * as signalR from "@microsoft/signalr"
import Sensor from './Sensor';
import Actor from './Actor';
import Map from './Map';

export default class PlaygroundService {
    constructor(playgroundViewRef, canvasRef, width, height) {
        this.playgroundViewRef = playgroundViewRef;
        this.canvasRef = canvasRef;

        this.playgroundWidth = width;
        this.playgroundHeight = height;

        this.map = new Map(100, 100, 150);
    }

    initialize() {
        1
        var green = 0x00ff00;
        var red = 0xff0000;
        var yellow = "#ffff00";

        var connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:5001/hub")
            .build()

        connection.on("ReceiveNewPopulation", () => {
            actors.forEach(actor => {
                actor.rotate(0);
                actor.setPosition(225, 80);
                actor.isAlive = true;
                actor.fitness = 0;
            });
        });

        connection.on("ReceiveAngle", (id, angle) => {
            actors[id].angle = angle * 180;
            actors[id].translateActor(new Phaser.Math.Vector2(1, 1));
        });

        function angle(id, sensors) {
            connection
                .invoke("GetAngle", id, sensors)
                .catch(error => console.log(error.toString()));
        }

        function setFitness(id, fitness) {
            connection
                .invoke("SetFitness", id, fitness)
                .catch(error => console.log(error.toString()));
        }

        function generateNewPopulation() {
            connection
                .invoke("GenerateNewPopulation")
                .catch(error => console.log(error.toString()));
        }

        connection
            .start()
            .then(() => console.log("Sudoku component connection started."))
            .catch(error => console.log(error))

        var graphics;

        function getActor(id) {
            return new Actor(id, 225, 80, 0, new Phaser.Geom.Circle(null, null, 10), [
                new Sensor("Sensor 1", -90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 2", -45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 3", -15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 4", 15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 5", 45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
                new Sensor("Sensor 6", 90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
            ]);
        }

        var actors = [
            getActor(0),
            getActor(1),
            getActor(2),
            getActor(3),
            getActor(4),
            getActor(5),
            getActor(6),
            getActor(7),
            getActor(8),
            getActor(9),
        ];

        var text;
        var walls = this.map.draw1();
        var finishLine = this.map.drawFinishLine();

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
            var allDead = false;
            for (let index = 0; index < actors.length; index++) {
                const actor = actors[index];
                if (actor.isAlive === false) {
                    allDead = true;
                }
                else {
                    allDead = false;
                    break;
                }
            }

            if (allDead) generateNewPopulation();

            graphics.clear();
            graphics.lineStyle(2, green);

            walls.forEach(wall => {
                graphics.strokeLineShape(wall);
            });

            graphics.lineStyle(1, 0xffff00);
            graphics.strokeLineShape(finishLine);

            drawActor();

            var currentDistance = Phaser.Math.Distance.Between(actors[0].x, actors[0].y, finishLine.x1, finishLine.y2 - (finishLine.y2 - finishLine.y1) / 2);
            var maxDistance = Phaser.Math.Distance.Between(225, 80, finishLine.x1, finishLine.y2 - (finishLine.y2 - finishLine.y1) / 2);

            text.setText(
                `${1 - currentDistance/maxDistance} Checkpoints: ${actors[0].fitness}|Alive:${actors[0].isAlive}\n` +
                actors[0].sensors.map(sensor => `${sensor.toString()}`).join("\n"));

            hangleKeyboardInputs();
            countCheckpoints()
        }

        function getDistance() {
            var currentDistance = Phaser.Math.Distance.Between(actors[0].x, actors[0].y, finishLine.x1, finishLine.y1);
            var maxDistance = Phaser.Math.Distance.Between(225, 80, finishLine.x1, finishLine.y1);

            return currentDistance;//1 - (currentDistance / maxDistance);
        }

        function countCheckpoints() {
            // for (let index = 0; index < checkpoints.length; index++) {
            //     const checkpoint = checkpoints[index];

            //     for (let j = 0; j < actors.length; j++) {
            //         const actor = actors[j];
            //         if (Phaser.Geom.Intersects.LineToCircle(checkpoint, actor.actorObject)) {
            //             actor.fitness = (index + 1) / checkpoints.length;
            //             break;
            //         }
            //     }
            // }
        }

        function getDistance(d) {
            return 1 - (d ?? 0.0);
        }

        function hangleKeyboardInputs() {

            // actors.forEach(actor => {
            //     if (actor.isAlive)
            //         angle(actor.id, actor.sensors.map(sensor => getDistance(sensor.collisionCoordinate.distance)));
            //     actor.setSensorsPosition();
            // });

            if (keyUp.isDown) {
                // actors.forEach(actor => {
                //     actor.isAlive = false;
                // });
                var translationVector = new Phaser.Math.Vector2(1, 1);
                actors[0].translateActor(translationVector);
            }
            if (keyDown.isDown) {
                var translationVector = new Phaser.Math.Vector2(-1, 1);
                actors[0].translateActor(translationVector);
            }
            if (keyLeft.isDown) {
                actors[0].rotate(actors[0].angle - 1);
            }
            if (keyRight.isDown) {
                actors[0].rotate(actors[0].angle + 1);
            }
        }

        function drawActor() {
            graphics.lineStyle(2, red);
            actors.forEach(actor => {
                graphics.strokeCircleShape(actor.actorObject);
                actor.sensors.forEach(sensor => {
                    graphics.strokeLineShape(sensor.sensorLine);
                });
            });
            drawCollisionPoints();
        }

        function drawCollisionPoints() {
            actors.forEach(actor => {
                for (let index = 0; index < actor.sensors.length; index++) {
                    const sensor = actor.sensors[index];

                    for (let index = 0; index < walls.length; index++) {
                        const wall = walls[index];

                        if (actor.isAlive && Phaser.Geom.Intersects.LineToCircle(wall, actor.actorObject)) {
                            actor.isAlive = false;
                            setFitness(actor.id, actor.fitness);
                        }

                        var intersection = Phaser.Geom.Intersects.GetLineToLine(sensor.sensorLine, wall);
                        if (intersection?.z <= 1) {
                            sensor.setCollision(intersection?.x, intersection?.y, intersection?.z)
                            graphics.strokeCircleShape(sensor.collisionCircle);
                            break;
                        }
                        else sensor.reset()
                    }
                }
            });
        }

        function createKeyboardInputs(input) {
            keyUp = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.UP);
            keyDown = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.DOWN);
            keyLeft = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.LEFT);
            keyRight = input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.RIGHT);
        }
    }
}
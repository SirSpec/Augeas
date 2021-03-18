import Phaser from 'phaser'
import * as signalR from "@microsoft/signalr"
import Actor from './Actor';
import Map from './Map';

import Configuration from "./Configuration"

export default class PlaygroundService {
    constructor(playgroundViewRef, canvasRef, width, height) {
        this.playgroundViewRef = playgroundViewRef;
        this.canvasRef = canvasRef;

        this.playgroundWidth = width;
        this.playgroundHeight = height;

        this.map = new Map(100, 100, 150);
    }

    initialize() {
        var green = 0x00ff00;
        var red = 0xff0000;
        var yellow = "#ffff00";

        var connection = new signalR.HubConnectionBuilder()
            .withUrl(Configuration.HubUrl)
            .build();

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

        connection.start()
            .then(() => console.log("Hermes client connection started."))
            .catch(error => console.log(error));

        var graphics;
        var actors;
        var text;
        var walls = this.map.getWalls();
        var finishLine = this.map.drawFinishLine();
        var maxDistance = Phaser.Math.Distance.Between(
            225, 80,
            finishLine.x1, finishLine.y2 - (finishLine.y2 - finishLine.y1) / 2);

        var keySpace;
        var keyUp;
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
            actors = [
                Actor.create(0),
                Actor.create(1),
                Actor.create(2),
                Actor.create(3),
                Actor.create(4),
                Actor.create(5),
                Actor.create(6),
                Actor.create(7),
                Actor.create(8),
                Actor.create(9),
            ];

            keySpace = this.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.SPACE);
            keyUp = this.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.UP);
            keyLeft = this.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.LEFT);
            keyRight = this.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.RIGHT);

            text = this.add.text(10, 800, '', { font: '16px Courier', fill: yellow });
        }

        function update() {
            drawMap();
            calculateCollisionPoints();
            hangleKeyboardInputs();
        }

        function drawMap() {
            graphics.clear();
            graphics.lineStyle(2, green);

            walls.forEach(wall => {
                graphics.strokeLineShape(wall);
            });

            graphics.lineStyle(1, 0xffff00);
            graphics.strokeLineShape(finishLine);

            text.setText(
                `Fitness: ${actors[0].fitness}|Alive:${actors[0].isAlive}\n` +
                actors[0].sensors.map(sensor => `${sensor.toString()}`).join("\n"));
        }

        function calculateCollisionPoints() {
            graphics.lineStyle(2, red);

            actors.forEach(actor => {
                if (actor.isAlive)
                    angle(actor.id, actor.sensors.map(sensor => sensor.collisionCoordinate?.distance ?? 1.0));
                actor.setSensorsPosition();

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
                        } else sensor.reset();
                    }
                }

                drawActor(actor);
                calculateFitness(actor);
            });

            if (actors.every(actor => !actor.isAlive)) generateNewPopulation();
        }

        function drawActor(actor) {
            graphics.strokeCircleShape(actor.actorObject);
            actor.sensors.forEach(sensor => {
                graphics.strokeLineShape(sensor.sensorLine);
            });
        }

        function calculateFitness(actor) {
            var currentDistance = Phaser.Math.Distance.Between(
                actor.x, actor.y,
                finishLine.x1, finishLine.y2 - (finishLine.y2 - finishLine.y1) / 2);
            actor.fitness = 1 - currentDistance / maxDistance;
        }

        function hangleKeyboardInputs() {
            if (keySpace.isDown) {
                actors.forEach(actor => {
                    actor.isAlive = false;
                });
            }
            if (keyUp.isDown) {
                var translationVector = new Phaser.Math.Vector2(1, 1);
                actors[0].translateActor(translationVector);
            }
            if (keyLeft.isDown) {
                actors[0].rotate(actors[0].angle - 1);
            }
            if (keyRight.isDown) {
                actors[0].rotate(actors[0].angle + 1);
            }
        }
    }
}
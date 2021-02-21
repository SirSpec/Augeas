import Phaser from 'phaser'

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
        var keys = [];

        document.body.addEventListener("keydown", (event) => {
            keys[event.keyCode] = true;
        });

        document.body.addEventListener("keyup", (event) => {
            keys[event.keyCode] = false;
        });

        var config = {
            width: this.playgroundWidth,
            height: this.playgroundHeight,
            type: Phaser.AUTO,
            parent: 'playground',
            scene: {
                create: create,
                update: update
            }
        };

        var x = 300;
        var y = 300;
        var sensorWidth = 100;

        var rect;

        var graphics;

        var actor;
        var sensor1;
        var sensor2;
        var sensor3;
        var sensor4;
        var sensor5;
        var sensor6;

        var degree = 0;
        var getAngleWithOffset = (offset) => (offset + degree) * Math.PI / 180;

        var game = new Phaser.Game(config);

        function create() {
            graphics = this.add.graphics();
            rect = new Phaser.Geom.Rectangle(50, 10, 1000, 20); //Remove

            createObjects();
            setSensorsPosition();

            this.input.keyboard.on('keydown-A', function () {
                degree = degree + 1;
                setSensorsPosition();
            }, this);

            this.input.keyboard.on('keydown-W', function () {
                translateActor();
            }, this);

            function createObjects() {
                actor = new Phaser.Geom.Circle(x, y, 10);
                sensor1 = new Phaser.Geom.Line();
                sensor2 = new Phaser.Geom.Line();
                sensor3 = new Phaser.Geom.Line();
                sensor4 = new Phaser.Geom.Line();
                sensor5 = new Phaser.Geom.Line();
                sensor6 = new Phaser.Geom.Line();
            }

            function translateActor() {
                var translationVector = new Phaser.Math.Vector2(1, 1);

                translationVector.setAngle(getAngleWithOffset(0));
                Phaser.Geom.Circle.Offset(actor, translationVector.x, translationVector.y);
                Phaser.Geom.Line.Offset(sensor1, translationVector.x, translationVector.y);
                Phaser.Geom.Line.Offset(sensor2, translationVector.x, translationVector.y);
                Phaser.Geom.Line.Offset(sensor3, translationVector.x, translationVector.y);
                Phaser.Geom.Line.Offset(sensor4, translationVector.x, translationVector.y);
                Phaser.Geom.Line.Offset(sensor5, translationVector.x, translationVector.y);
                Phaser.Geom.Line.Offset(sensor6, translationVector.x, translationVector.y);

                x = actor.x;
                y = actor.y;
            }

            function setSensorsPosition() {
                Phaser.Geom.Line.SetToAngle(sensor1, x, y, getAngleWithOffset(90), sensorWidth);
                Phaser.Geom.Line.SetToAngle(sensor2, x, y, getAngleWithOffset(45), sensorWidth);
                Phaser.Geom.Line.SetToAngle(sensor3, x, y, getAngleWithOffset(15), sensorWidth);
                Phaser.Geom.Line.SetToAngle(sensor4, x, y, getAngleWithOffset(-15), sensorWidth);
                Phaser.Geom.Line.SetToAngle(sensor5, x, y, getAngleWithOffset(-45), sensorWidth);
                Phaser.Geom.Line.SetToAngle(sensor6, x, y, getAngleWithOffset(-90), sensorWidth);
            }
        }

        function update() {

            graphics.clear();

            graphics.strokeCircleShape(actor);

            graphics.lineStyle(2, 0x00ff00);
            graphics.strokeLineShape(sensor1);
            graphics.strokeLineShape(sensor2);
            graphics.strokeLineShape(sensor3);
            graphics.strokeLineShape(sensor4);
            graphics.strokeLineShape(sensor5);
            graphics.strokeLineShape(sensor6);

            var inte = []

            if (Phaser.Geom.Intersects.GetLineToRectangle(sensor1, rect, inte)) {
                graphics.lineStyle(2, 0xff0000);
            }
            else {
                graphics.lineStyle(2, 0xffff00);
            }

            graphics.strokeRectShape(rect, 2);

            if (inte[0]) {
                console.log(inte[0]?.x + " " + inte[0]?.y)
            }
        }
    }
}
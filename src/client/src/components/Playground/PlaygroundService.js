import Phaser from 'phaser'

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
        var yellow = 0xffff00;

        var sensorWidth = 100;
        var x = 300;
        var y = 300;
        var degree = 0;

        var graphics;

        var actor;
        var sensor1;
        var sensor2;
        var sensor3;
        var sensor4;
        var sensor5;
        var sensor6;

        var collision1;
        var collision2;
        var collision3;
        var collision4;
        var collision5;
        var collision6;

        var text;

        var inte1
        var inte2
        var inte3
        var inte4
        var inte5
        var inte6

        var wall;

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

        function update() {
            graphics.clear();
            hangleKeyboardInputs();

            graphics.strokeCircleShape(actor);

            graphics.lineStyle(2, green);
            graphics.strokeLineShape(sensor1);
            graphics.strokeLineShape(sensor2);
            graphics.strokeLineShape(sensor3);
            graphics.strokeLineShape(sensor4);
            graphics.strokeLineShape(sensor5);
            graphics.strokeLineShape(sensor6);

            drawCollisionPoints();


            graphics.strokeLineShape(wall);

            text.setText("Sensor1:" + inte1?.x + ", " + inte1?.y + ": " + inte1?.z + "\n" +
                "Sensor2:" + inte2?.x + ", " + inte2?.y + ": " + inte2?.z + "\n" +
                "Sensor3:" + inte3?.x + ", " + inte3?.y + ": " + inte3?.z + "\n" +
                "Sensor4:" + inte4?.x + ", " + inte4?.y + ": " + inte4?.z + "\n" +
                "Sensor5:" + inte5?.x + ", " + inte5?.y + ": " + inte5?.z + "\n" +
                "Sensor6:" + inte6?.x + ", " + inte6?.y + ": " + inte6?.z + "\n");
        }

        function drawCollisionPoints() {
            inte1 = undefined;
            inte2 = undefined;
            inte3 = undefined;
            inte4 = undefined;
            inte5 = undefined;
            inte6 = undefined;

            if (Phaser.Geom.Intersects.LineToLine(sensor1, wall)) {
                inte1 = Phaser.Geom.Intersects.GetLineToLine(sensor1, wall);
                collision1.setPosition(inte1?.x, inte1?.y);
                graphics.strokeCircleShape(collision1);
            }

            if (Phaser.Geom.Intersects.LineToLine(sensor2, wall)) {
                inte2 = Phaser.Geom.Intersects.GetLineToLine(sensor2, wall);
                collision2.setPosition(inte2?.x, inte2?.y);
                graphics.strokeCircleShape(collision2);
            }

            if (Phaser.Geom.Intersects.LineToLine(sensor3, wall)) {
                inte3 = Phaser.Geom.Intersects.GetLineToLine(sensor3, wall);
                collision3.setPosition(inte3?.x, inte3?.y);
                graphics.strokeCircleShape(collision3);
            }

            if (Phaser.Geom.Intersects.LineToLine(sensor4, wall)) {
                inte4 = Phaser.Geom.Intersects.GetLineToLine(sensor4, wall);
                collision4.setPosition(inte4?.x, inte4?.y);
                graphics.strokeCircleShape(collision4);
            }

            if (Phaser.Geom.Intersects.LineToLine(sensor5, wall)) {
                inte5 = Phaser.Geom.Intersects.GetLineToLine(sensor5, wall);
                collision5.setPosition(inte5?.x, inte5?.y);
                graphics.strokeCircleShape(collision5);
            }

            if (Phaser.Geom.Intersects.LineToLine(sensor6, wall)) {
                inte6 = Phaser.Geom.Intersects.GetLineToLine(sensor6, wall);
                collision6.setPosition(inte6?.x, inte6?.y);
                graphics.strokeCircleShape(collision6);
            }

            if (inte1) {
                console.log(inte1?.x + " " + inte1?.y)
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
            sensor1 = new Phaser.Geom.Line();
            sensor2 = new Phaser.Geom.Line();
            sensor3 = new Phaser.Geom.Line();
            sensor4 = new Phaser.Geom.Line();
            sensor5 = new Phaser.Geom.Line();
            sensor6 = new Phaser.Geom.Line();

            setSensorsPosition();
        }

        function createCollisionPoints() {
            collision1 = new Phaser.Geom.Circle(null, null, 10);
            collision2 = new Phaser.Geom.Circle(null, null, 10);
            collision3 = new Phaser.Geom.Circle(null, null, 10);
            collision4 = new Phaser.Geom.Circle(null, null, 10);
            collision5 = new Phaser.Geom.Circle(null, null, 10);
            collision6 = new Phaser.Geom.Circle(null, null, 10);
        }

        function createMap() {
            wall = new Phaser.Geom.Line(50, 10, 1000, 20);
        }

        var getAngleWithOffset = (offset) => (offset + degree) * Math.PI / 180;

        function translateActor(translationVector) {
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

        function hangleKeyboardInputs() {
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
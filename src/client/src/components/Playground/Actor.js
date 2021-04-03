import Phaser from 'phaser';
import Sensor from './Sensor';

export default class Actor {
	constructor(id, x, y, angle, actorObject, sensors) {
		this.id = id;
		this.x = x;
		this.y = y;
		this.angle = angle;
		this.isAlive = true;
		this.fitness = 0;

		this.actorObject = actorObject;
		this.actorObject.setPosition(this.x, this.y);
		this.sensors = sensors;
		this.setSensorsPosition();
	}

	resetToPosition(x, y) {
		this.rotate(0);
		this.setPosition(x, y);
		this.isAlive = true;
		this.fitness = 0;
	}

	setPosition(x, y) {
		this.x = x;
		this.y = y;
		this.actorObject.setPosition(this.x, this.y);
	}

	rotate(angle) {
		this.angle = angle;
		this.setSensorsPosition();
	}

	getAngleWithOffset(offset) {
		return (offset + this.angle) * Math.PI / 180;
	}

	translateActor(translationVector) {
		translationVector.setAngle(this.getAngleWithOffset(0));
		Phaser.Geom.Circle.Offset(this.actorObject, translationVector.x, translationVector.y);

		this.sensors.forEach(sensor => {
			Phaser.Geom.Line.Offset(sensor.sensorLine, translationVector.x, translationVector.y);
		});

		this.x = this.actorObject.x;
		this.y = this.actorObject.y;
	}

	setSensorsPosition() {
		this.sensors.forEach(sensor => {
			Phaser.Geom.Line.SetToAngle(sensor.sensorLine, this.x, this.y, sensor.getAngle(this.angle), sensor.width);
		});
	}

	static create(id) {
		return new Actor(id, 225, 80, 0, new Phaser.Geom.Circle(null, null, 10), [
			new Sensor("Sensor 1", -90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
			new Sensor("Sensor 2", -45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
			new Sensor("Sensor 3", -15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
			new Sensor("Sensor 4", 15, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
			new Sensor("Sensor 5", 45, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
			new Sensor("Sensor 6", 90, new Phaser.Geom.Line(), new Phaser.Geom.Circle(null, null, 10)),
		]);
	}
}

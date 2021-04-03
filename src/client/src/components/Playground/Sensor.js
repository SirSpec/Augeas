import CollisionCoordinate from './CollisionCoordinate';

export default class Sensor {
	constructor(id, offset, sensorLine, collisionCircle) {
		this.id = id;
		this.defaultWidth = 100;
		this.width = this.defaultWidth;
		this.collisionCoordinate = new CollisionCoordinate();
		this.offset = offset;
		this.sensorLine = sensorLine;
		this.collisionCircle = collisionCircle;
	}

	getAngle(angle) {
		return (this.offset + angle) * Math.PI / 180;
	}

	setCollision(x, y, distance) {
		if (x && y && distance) {
			this.collisionCoordinate.set(x, y, distance);
			this.collisionCircle.setPosition(this.collisionCoordinate.x, this.collisionCoordinate.y);
		}
	}

	isColliding() {
		return this.collisionCoordinate.x && this.collisionCoordinate.y && this.collisionCoordinate.distance
			? true
			: false;
	}

	reset() {
		this.collisionCoordinate.x = null;
		this.collisionCoordinate.y = null;
		this.collisionCoordinate.distance = null;
	}

	toString() {
		return `${this.id}: ${this.collisionCoordinate.toString()}`;
	}
}
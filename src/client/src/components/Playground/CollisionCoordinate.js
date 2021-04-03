export default class CollisionCoordinate {
	constructor() {
		this.x = null;
		this.y = null;
		this.distance = null;
	}

	set(x, y, distance) {
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
import Phaser from 'phaser';

export default class Map {
    constructor(centerStartX, centerStartY, width) {
        this.centerStartX = centerStartX;
        this.centerStartY = centerStartY;
        this.width = width;
    }

    getCorridor(distance, angle) {
        var firstStartX = this.centerStartX;
        var firstStartY = this.centerStartY - this.width / 2;

        var firstEndX = this.centerStartX + distance;
        var firstEndY = this.centerStartY - this.width / 2;

        var secondStartX = this.centerStartX;
        var secondStartY = this.centerStartY + this.width / 2;

        var secondEndX = this.centerStartX + distance;
        var secondEndY = this.centerStartY + this.width / 2;

        var firstLine = new Phaser.Geom.Line(firstStartX, firstStartY, firstEndX, firstEndY);
        Phaser.Geom.Line.SetToAngle(firstLine, firstStartX, firstStartY, angle * Math.PI / 180, distance);

        var secondLine = new Phaser.Geom.Line(secondStartX, secondStartY, secondEndX, secondEndY);
        Phaser.Geom.Line.SetToAngle(secondLine, secondStartX, secondStartY, angle * Math.PI / 180, distance);

        this.centerStartX = firstLine.x2;
        this.centerStartY = firstLine.y2 + this.width / 2;

        return [
            firstLine,
            secondLine
        ];
    }

    getWalls() {
        var output = [
            new Phaser.Geom.Line(
                this.centerStartX, this.centerStartY - this.width / 2,
                this.centerStartX, this.centerStartX + this.width / 2)
        ];
        output.push(...this.getCorridor(200, 0));
        output.push(...this.getCorridor(200, 45));
        output.push(...this.getCorridor(200, -35));
        output.push(...this.getCorridor(200, 0));
        output.push(...this.getCorridor(300, 45));
        output.push(...this.getCorridor(260, 20));
        output.push(...this.getCorridor(150, 0));

        return output;
    }

    drawTestMap() {
        return [
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
    }

    drawFinishLine() {
        return new Phaser.Geom.Line(1400, 350, 1400, 500);
    }
}

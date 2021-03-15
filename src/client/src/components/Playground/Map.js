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

    draw1() {
        var output = [];
        output.push(...this.getCorridor(200, 0));
        output.push(...this.getCorridor(200, 45));
        output.push(...this.getCorridor(200, -35));
        output.push(...this.getCorridor(200, 0));
        output.push(...this.getCorridor(200, 90));

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

    drawTestCheckpoints() {
        return [
            new Phaser.Geom.Line(300, 20, 300, 140),
            new Phaser.Geom.Line(400, 20, 400, 140),
            new Phaser.Geom.Line(500, 20, 500, 140),
            new Phaser.Geom.Line(600, 20, 600, 140),
            new Phaser.Geom.Line(700, 20, 700, 140),
            new Phaser.Geom.Line(800, 20, 800, 140),
            new Phaser.Geom.Line(900, 20, 900, 140),
            new Phaser.Geom.Line(1000, 20, 1000, 140),
            new Phaser.Geom.Line(1100, 20, 1100, 140),
            new Phaser.Geom.Line(1200, 20, 1200, 140),
            new Phaser.Geom.Line(1300, 20, 1300, 140),

            new Phaser.Geom.Line(1310, 150, 1390, 150),
            new Phaser.Geom.Line(1310, 200, 1390, 200),
            new Phaser.Geom.Line(1310, 250, 1390, 250),
            new Phaser.Geom.Line(1310, 350, 1390, 350),
            new Phaser.Geom.Line(1310, 450, 1390, 450),
            new Phaser.Geom.Line(1310, 550, 1390, 550),
            new Phaser.Geom.Line(1310, 650, 1390, 650),

            new Phaser.Geom.Line(1200, 710, 1200, 790),
            new Phaser.Geom.Line(1100, 710, 1100, 790),
            new Phaser.Geom.Line(1000, 710, 1000, 790),

            new Phaser.Geom.Line(860, 700, 990, 700),
            new Phaser.Geom.Line(860, 600, 990, 600),
            new Phaser.Geom.Line(860, 500, 990, 500),
            new Phaser.Geom.Line(860, 400, 990, 400),

            new Phaser.Geom.Line(850, 260, 850, 390),
            new Phaser.Geom.Line(750, 260, 750, 390),
            new Phaser.Geom.Line(650, 260, 650, 390),

            new Phaser.Geom.Line(510, 400, 640, 400),
            new Phaser.Geom.Line(510, 500, 640, 500),
            new Phaser.Geom.Line(510, 600, 640, 600),
            new Phaser.Geom.Line(510, 700, 640, 700),

            new Phaser.Geom.Line(500, 710, 500, 790),
            new Phaser.Geom.Line(400, 710, 400, 790),
            new Phaser.Geom.Line(300, 710, 300, 790),
            new Phaser.Geom.Line(200, 710, 200, 790),

            new Phaser.Geom.Line(20, 700, 140, 700),
            new Phaser.Geom.Line(20, 600, 140, 600),
            new Phaser.Geom.Line(20, 500, 140, 500),
            new Phaser.Geom.Line(20, 400, 140, 400),
            new Phaser.Geom.Line(20, 300, 140, 300),
            new Phaser.Geom.Line(20, 200, 140, 200),
            new Phaser.Geom.Line(20, 100, 140, 100),
        ];
    }
}

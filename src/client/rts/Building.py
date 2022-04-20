from random import random

import arcade
import math
import random

from Unit import Unit

class Building(arcade.SpriteCircle):
    BLUE_BUILDING_COLOR_RGB = (52, 209, 191)
    RED_BUILDING_COLOR_RGB = (209, 52, 91)

    BUILDING_RADIUS = 35
    BUILDING_BASE_HEALTH = 1000

    def __init__(self, name: str, color, center_x: float = 0, center_y: float = 0) -> None:
        super().__init__(radius=Building.BUILDING_RADIUS, color=color)
        self.name = name
        self.health = Building.BUILDING_BASE_HEALTH

        self.center_x = center_x
        self.center_y = center_y
        self.alpha = 200

    def take_damage(self, damage: float) -> None:
        if not self.is_destroyed():
            if self.health - damage > 0:
                self.health -= damage
            else:
                self.health = 0
                super().kill()

    def is_destroyed(self) -> bool:
        return self.health == 0

    def build_unit(self, name, sprite_name) -> Unit:
        x, y = Building.get_position(self.center_x, self.center_y, 50., random.random() * 360)
        return Unit(name, sprite_name, x, y)

    def get_position(x: float, y: float, distance: float, angleDegrees: float):
        angle_radians = math.pi/2 - math.radians(angleDegrees)
        return x + distance * math.cos(angle_radians), y + distance * math.sin(angle_radians)

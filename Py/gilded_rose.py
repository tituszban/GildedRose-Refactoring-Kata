CONJURED = "Conjured Mana Cake"
BACKSTAGE_PASSES = "Backstage passes to a TAFKAL80ETC concert"
AGED_BRIE = "Aged Brie"
SULFURAS = "Sulfuras, Hand of Ragnaros"
DEFAULT = "default"


class GildedRose(object):

    def __init__(self, items):
        self.items = items
        self.sell_in_change = {
            DEFAULT: -1,
            SULFURAS: 0
        }
        self.quality_change = {
            DEFAULT:
                lambda item: -1,
            SULFURAS:
                lambda item: 0,
            AGED_BRIE:
                lambda item: 1,
            BACKSTAGE_PASSES:
                lambda item:
                ([value for key, value in {-1: -item.quality, 4: 3, 9: 2}.items() if item.sell_in <= key] or [1])[0]
        }
        self.change_multiplier = {
            DEFAULT:
                lambda item: (1 if item.sell_in >= 0 else 2),
            CONJURED:
                lambda item: (2 if item.sell_in >= 0 else 4)
        }

        self.limit_quality = {
            DEFAULT:
                lambda item: min(max(item.quality, 0), 50),
            SULFURAS:
                lambda item: item.quality
        }

    def update_item_sell_in(self, item):
        item.sell_in += self.sell_in_change.get(item.name, self.sell_in_change[DEFAULT])

    def update_item_quality(self, item):
        item.quality += \
            self.quality_change.get(item.name, self.quality_change[DEFAULT])(item) * \
            self.change_multiplier.get(item.name, self.change_multiplier[DEFAULT])(item)

        item.quality = self.limit_quality.get(item.name, self.limit_quality[DEFAULT])(item)

    def update_quality(self):
        for item in self.items:
            self.update_item_sell_in(item)
            self.update_item_quality(item)


class Item:
    def __init__(self, name, sell_in, quality):
        self.name = name
        self.sell_in = sell_in
        self.quality = quality

    def __repr__(self):
        return "%s, %s, %s" % (self.name, self.sell_in, self.quality)

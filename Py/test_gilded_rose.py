# -*- coding: utf-8 -*-
import unittest

from gilded_rose import Item, GildedRose

SELL_IN = "sell_in"
QUALITY = "quality"
NAME = "name"

# ITEMS
DEXTERITY_VEST_5 = "+5 Dexterity Vest"
AGED_BRIE = "Aged Brie"
ELIXIR_MONGOOSE = "Elixir of the Mongoose"
SULFURAS_HAND_OF_RAGNAROS = "Sulfuras, Hand of Ragnaros"
BACKSTAGE_PASS_TAFKAL = "Backstage passes to a TAFKAL80ETC concert"
CONJURED_MANNA_CAKE = "Conjured Mana Cake"

NORMAL_ITEM = DEXTERITY_VEST_5


# source: https://eli.thegreenplace.net/2011/08/02/python-unit-testing-parametrized-test-cases
class ParametrizedTestCase(unittest.TestCase):
    """ TestCase classes that want to be parametrized should
        inherit from this class.
    """

    def __init__(self, methodName='runTest', param={}):
        super(ParametrizedTestCase, self).__init__(methodName)
        self.param = param

    @staticmethod
    def parametrize(testcase_klass, param):
        """ Create a suite containing all tests taken from the given
            subclass, passing them the parameter 'param'.
        """
        testloader = unittest.TestLoader()
        testnames = testloader.getTestCaseNames(testcase_klass)
        suite = unittest.TestSuite()
        for name in testnames:
            suite.addTest(testcase_klass(name, param))
        return suite


class BasicRulesTestCase(unittest.TestCase):

    def __init__(self, methodName='runTest', param={}):
        super(BasicRulesTestCase, self).__init__(methodName)
        assert NAME in param
        self.param = param

    def setUp(self):
        self.item_start_properties = {NAME: self.param[NAME], SELL_IN: 20, QUALITY: 20}
        self.item = Item(**self.item_start_properties)
        GildedRose([self.item]).update_quality()

    def test_sell_in_reduced(self):
        exceptions = [SULFURAS_HAND_OF_RAGNAROS]
        if self.param[NAME] in exceptions:
            return
        self.assertEqual(getattr(self.item, SELL_IN), self.item_start_properties[SELL_IN] - 1, self.param[NAME])

    def test_quality_reduced(self):
        exceptions = [AGED_BRIE, SULFURAS_HAND_OF_RAGNAROS, BACKSTAGE_PASS_TAFKAL, CONJURED_MANNA_CAKE]
        if self.param[NAME] in exceptions:
            return
        self.assertEqual(getattr(self.item, QUALITY), self.item_start_properties[QUALITY] - 1, self.param[NAME])

    def test_name_unchanged(self):
        exceptions = []
        if self.param[NAME] in exceptions:
            return
        self.assertEqual(getattr(self.item, NAME), self.item_start_properties[NAME], self.param[NAME])


class DoubleQualityReductionAfterSellByTestCase(unittest.TestCase):
    def __init__(self, methodName='runTest', param={}):
        super(DoubleQualityReductionAfterSellByTestCase, self).__init__(methodName)
        assert NAME in param
        self.param = param

    def setUp(self):
        self.item_start_properties = {NAME: self.param[NAME], SELL_IN: -1, QUALITY: 5}
        self.baseline_item_start_properties = {NAME: self.param[NAME], SELL_IN: 20, QUALITY: 5}
        self.item = Item(**self.item_start_properties)
        self.baseline_item = Item(**self.baseline_item_start_properties)
        GildedRose([self.item, self.baseline_item]).update_quality()

    def test_double_quality_reduction(self):
        exceptions = [BACKSTAGE_PASS_TAFKAL]
        if self.param[NAME] in exceptions:
            return
        delta_quality = getattr(self.item, QUALITY) - self.item_start_properties[QUALITY]
        baseline_delta_quality = getattr(self.baseline_item, QUALITY) - self.baseline_item_start_properties[QUALITY]

        self.assertEqual(delta_quality // 2, baseline_delta_quality, self.param[NAME])


class QualityLimitTestCase(unittest.TestCase):
    def __init__(self, methodName='runTest', param={}):
        super(QualityLimitTestCase, self).__init__(methodName)
        assert NAME in param
        self.param = param

    def setUp(self):
        self.properties_for_items = [
            {NAME: self.param[NAME], SELL_IN: 5, QUALITY: 0},
            {NAME: self.param[NAME], SELL_IN: 0, QUALITY: 0},
            {NAME: self.param[NAME], SELL_IN: -5, QUALITY: 0},
            {NAME: self.param[NAME], SELL_IN: 5, QUALITY: 50},
            {NAME: self.param[NAME], SELL_IN: 0, QUALITY: 50},
            {NAME: self.param[NAME], SELL_IN: -5, QUALITY: 50},
        ]

        self.items = [Item(**properties) for properties in self.properties_for_items]

        GildedRose(self.items).update_quality()

    def test_greater_than_zero(self):
        exceptions = []
        if self.param[NAME] in exceptions:
            return
        for item in self.items:
            self.assertTrue(getattr(item, QUALITY) >= 0)

    def test_less_than_fifty(self):
        exceptions = []
        if self.param[NAME] in exceptions:
            return
        for item in self.items:
            self.assertTrue(getattr(item, QUALITY) <= 50)


class AgedBrieTestCase(unittest.TestCase):
    def setUp(self):
        self.properties_for_items = [
            {NAME: AGED_BRIE, SELL_IN: 5, QUALITY: 0},
            {NAME: AGED_BRIE, SELL_IN: 0, QUALITY: 0},
            {NAME: AGED_BRIE, SELL_IN: -5, QUALITY: 0},
            {NAME: AGED_BRIE, SELL_IN: 5, QUALITY: 40},
            {NAME: AGED_BRIE, SELL_IN: 0, QUALITY: 40},
            {NAME: AGED_BRIE, SELL_IN: -5, QUALITY: 40},
        ]

        self.items = [Item(**properties) for properties in self.properties_for_items]

        GildedRose(self.items).update_quality()

    def test_all_increased(self):
        for i, item in enumerate(self.items):
            self.assertTrue(getattr(item, QUALITY) > self.properties_for_items[i][QUALITY])


class SulfurasTestCase(unittest.TestCase):
    def setUp(self):
        self.properties_for_items = [
            {NAME: SULFURAS_HAND_OF_RAGNAROS, SELL_IN: 5, QUALITY: 0},
            {NAME: SULFURAS_HAND_OF_RAGNAROS, SELL_IN: 1, QUALITY: 0},
            {NAME: SULFURAS_HAND_OF_RAGNAROS, SELL_IN: 5, QUALITY: 40},
            {NAME: SULFURAS_HAND_OF_RAGNAROS, SELL_IN: 1, QUALITY: 40}
        ]
        self.items = [Item(**properties) for properties in self.properties_for_items]

        GildedRose(self.items).update_quality()

    def test_quality_unchanged(self):
        for i, item in enumerate(self.items):
            self.assertEqual(getattr(item, QUALITY), self.properties_for_items[i][QUALITY])

    def test_not_to_be_sold(self):
        for i, item in enumerate(self.items):
            self.assertTrue(getattr(item, SELL_IN) > 0)


class BackstagePassTestCase(unittest.TestCase):
    def setUp(self):
        self.start_sell_in = 15
        self.item_properties = {NAME: BACKSTAGE_PASS_TAFKAL, SELL_IN: self.start_sell_in, QUALITY: 1}
        self.item = Item(**self.item_properties)

        self.gilded_rose = GildedRose([self.item])

    def test_increase_regular(self):
        current_day = self.start_sell_in
        assert current_day > 10

        prev_quality = getattr(self.item, QUALITY)

        while current_day > 10:
            self.gilded_rose.update_quality()
            self.assertEqual(getattr(self.item, QUALITY), prev_quality + 1, current_day)
            prev_quality = getattr(self.item, QUALITY)
            current_day -= 1

    def test_increase_double(self):
        current_day = self.start_sell_in
        while current_day > 10:
            self.gilded_rose.update_quality()
            current_day -= 1

        prev_quality = getattr(self.item, QUALITY)
        while current_day > 5:
            self.gilded_rose.update_quality()
            self.assertEqual(getattr(self.item, QUALITY), prev_quality + 2, current_day)
            prev_quality = getattr(self.item, QUALITY)
            current_day -= 1

    def test_increase_triple(self):
        current_day = self.start_sell_in
        while current_day > 5:
            self.gilded_rose.update_quality()
            current_day -= 1

        prev_quality = getattr(self.item, QUALITY)
        while current_day > 0:
            self.gilded_rose.update_quality()
            self.assertEqual(getattr(self.item, QUALITY), prev_quality + 3, current_day)
            prev_quality = getattr(self.item, QUALITY)
            current_day -= 1

    def test_reduces_to_zero(self):
        current_day = self.start_sell_in
        while current_day > 0:
            self.gilded_rose.update_quality()
            current_day -= 1

        while current_day >= -5:
            self.gilded_rose.update_quality()
            self.assertEqual(getattr(self.item, QUALITY), 0, current_day)
            current_day -= 1

class ConjuredTestCases(unittest.TestCase):
    def setUp(self):
        self.properties_for_items = [
            {NAME: CONJURED_MANNA_CAKE, SELL_IN: 5, QUALITY: 10},
            {NAME: CONJURED_MANNA_CAKE, SELL_IN: 0, QUALITY: 10},
            {NAME: CONJURED_MANNA_CAKE, SELL_IN: -5, QUALITY: 10}
        ]
        self.properties_for_normal_items = [
            {NAME: NORMAL_ITEM, SELL_IN: 5, QUALITY: 10},
            {NAME: NORMAL_ITEM, SELL_IN: 0, QUALITY: 10},
            {NAME: NORMAL_ITEM, SELL_IN: -5, QUALITY: 10}
        ]

        self.items = [Item(**properties) for properties in self.properties_for_items]
        self.normal_items = [Item(**properties) for properties in self.properties_for_normal_items]

        GildedRose(self.items + self.normal_items).update_quality()

    def test_all_increased_double(self):
        assert len(self.items) == len(self.normal_items)
        for i in range(len(self.items)):
            change = getattr(self.items[i], QUALITY) - self.properties_for_items[i][QUALITY]
            normal_change = getattr(self.normal_items[i], QUALITY) - self.properties_for_normal_items[i][QUALITY]
            self.assertEqual(change, normal_change * 2, i)


ITEMS = (
    DEXTERITY_VEST_5,
    AGED_BRIE,
    ELIXIR_MONGOOSE,
    SULFURAS_HAND_OF_RAGNAROS,
    BACKSTAGE_PASS_TAFKAL,
    CONJURED_MANNA_CAKE
)


def run_test_suite():
    suite = unittest.TestSuite()

    for item in ITEMS:
        suite.addTest(ParametrizedTestCase.parametrize(BasicRulesTestCase, {NAME: item}))
        suite.addTest(ParametrizedTestCase.parametrize(DoubleQualityReductionAfterSellByTestCase, {NAME: item}))
        suite.addTest(ParametrizedTestCase.parametrize(QualityLimitTestCase, {NAME: item}))

    suite.addTest(AgedBrieTestCase("test_all_increased"))
    suite.addTest(SulfurasTestCase("test_quality_unchanged"))
    suite.addTest(SulfurasTestCase("test_not_to_be_sold"))
    suite.addTest(BackstagePassTestCase("test_increase_regular"))
    suite.addTest(BackstagePassTestCase("test_increase_double"))
    suite.addTest(BackstagePassTestCase("test_increase_triple"))
    suite.addTest(BackstagePassTestCase("test_reduces_to_zero"))
    suite.addTest(ConjuredTestCases("test_all_increased_double"))

    unittest.TextTestRunner(verbosity=2).run(suite)


if __name__ == '__main__':
    run_test_suite()

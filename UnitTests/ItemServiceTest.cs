using DomainModel;
using Moq;
using Repositories.ItemRepository;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ItemServiceTest
    {
        private readonly Mock<IItemRepository> _itemRepoMock = new Mock<IItemRepository>();
        private readonly ItemService _itemService;
        private List<Item> _mockItems;

        public ItemServiceTest()
        {
            _itemService = new ItemService(_itemRepoMock.Object);
            this._mockItems = new List<Item>
                    {
                        new Item(){ ItemId = 1, Text="testdata1", CreatedBy="97B57E6B-928D-4644-8708-0EF67E288CFC", DateCreated=DateTime.Now},
                        new Item(){ ItemId = 2, Text="testdata2", CreatedBy="C8BBBFA9-AAE3-495E-A33F-B0F1642CEB10", DateCreated=DateTime.Now},
                        new Item(){ ItemId = 3, Text="testdata3", CreatedBy="266EB3E6-AD86-4A19-9726-124AA8E8F51A", DateCreated=DateTime.Now}
                    };
        }

        [Fact]
        public void WhenGetAllItems_ShouldReturnItemsList()
        {
            // Arrange

            // Act
            var allitems = _itemService.GetAll();

            // Assert
            Assert.NotNull(allitems);
        }

        [Fact]
        public void WhenGetByItemId_ShouldReturnItemDto()
        {
            // Arrange
            int itemId = 2;
            _itemRepoMock.Setup(_ => _.All()).Returns(this._mockItems.AsQueryable());

            // Act
            var item = _itemService.Get(itemId);

            //Assert
            Assert.Equal(itemId, item.ItemId);
        }

        [Fact]
        public void WhenItemExists_ShouldReturnTrue()
        {
            // Arrange
            int itemId = 2;
            _itemRepoMock.Setup(_ => _.All()).Returns(this._mockItems.AsQueryable());

            // Act
            bool itemExist = _itemService.ItemExist(itemId);
            bool expected = true;

            //Assert
            Assert.Equal(expected, itemExist);
        }

        [Fact]
        public void WhenItemDoesNotExists_ShouldReturnFalse()
        {
            // Arrange
            int itemId = 100;
            _itemRepoMock.Setup(_ => _.All()).Returns(this._mockItems.AsQueryable());

            // Act
            bool itemExist = _itemService.ItemExist(itemId);
            bool expected = false;

            //Assert
            Assert.Equal(expected, itemExist);
        }
    }
}

using BridgeIntServices.Controllers;
using BridgeIntServices.Core.Interfaces;
using BridgeIntServices.Core.Models;
using BridgeIntServices.Interfaces;
using BridgeIntServices.Models;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BridgeIntServices.Test.ControllerTests
{
    [TestClass]
    public class BatteryUsageControllerTests
    {
        private readonly Mock<IBatteryUsageLogics> _mockBatteryUsageLogics = new Mock<IBatteryUsageLogics>();
        private readonly Mock<IResponseHelper> _mockResponseHelper = new Mock<IResponseHelper>();
        private BatteryUsageController _controller;
        private List<DeviceTrackPoint> _deviceTrackPointList;
        private List<BatteryUsage> _batteryUsageList;
        private ResponseObject _resObject;
        private ObjectResult _objectResult;

        [TestInitialize]
        public void TestInitialization()
        {
            _controller = new BatteryUsageController(_mockBatteryUsageLogics.Object, _mockResponseHelper.Object);
            _deviceTrackPointList = Builder<DeviceTrackPoint>.CreateListOfSize(2).Build().ToList();
            _batteryUsageList = Builder<BatteryUsage>.CreateListOfSize(2).Build().ToList();
            _resObject = Builder<ResponseObject>.CreateNew().Build();
            _resObject.Data = _batteryUsageList;
        }

        [TestMethod]
        public async Task CalculateBatteryDailyAverage_Sucessfully()
        {
            _mockBatteryUsageLogics.Setup(x => x.CalculateBatteryDailyAverage(It.IsAny<List<DeviceTrackPoint>>())).ReturnsAsync(_resObject);

            var resultRes = await _controller.CalculateBatteryDailyAverage(_deviceTrackPointList);

            Assert.IsNotNull(resultRes);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)resultRes).StatusCode);
        }

        [TestMethod]
        public async Task CalculateBatteryDailyAverage_Throw_Exception()
        {
            _resObject.Message = "Test Exception";
            _resObject.ResponseCode = new int?(500);
            _objectResult = new ObjectResult(_resObject);

            _mockResponseHelper.Setup(x => x.CreateApiError(It.IsAny<Exception>())).Returns(_objectResult);
            _mockBatteryUsageLogics.Setup(x => x.CalculateBatteryDailyAverage(It.IsAny<List<DeviceTrackPoint>>())).ThrowsAsync(new Exception("Test Exception"));

            var resultRes = await _controller.CalculateBatteryDailyAverage(_deviceTrackPointList);

            Assert.IsNotNull(resultRes);
            Assert.AreEqual("Test Exception", ((ResponseObject)((ObjectResult)resultRes).Value).Message);
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, ((ResponseObject)((ObjectResult)resultRes).Value).ResponseCode);
        }
    }
}

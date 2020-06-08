using BridgeIntServices.Core.Interfaces;
using BridgeIntServices.Core.Models;
using BridgeIntServices.Logics;
using BridgeIntServices.Models;
using BridgeIntServices.Repositories.Interfaces;
using FizzWare.NBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgeIntServices.Test.LogicTests
{
    [TestClass]
    public class BatteryUsageLogicsTests
    {
        private readonly Mock<IResponseHelper> _mockResponseHelper = new Mock<IResponseHelper>();
        private readonly Mock<IBatteryUsageRepository> _mockBatteryUsageRepository = new Mock<IBatteryUsageRepository>();
        private BatteryUsageLogics _batteryUsageLogics;
        private List<BatteryUsage> _batteryUsageList;
        private List<DeviceTrackPoint> _deviceTrackPointList;
        private ResponseObject _resObject;

        [TestInitialize]
        public void TestInitialization()
        {
            _batteryUsageLogics = new BatteryUsageLogics(_mockResponseHelper.Object, _mockBatteryUsageRepository.Object);
            _batteryUsageList = Builder<BatteryUsage>.CreateListOfSize(2).Build().ToList();
            _deviceTrackPointList = Builder<DeviceTrackPoint>.CreateListOfSize(2).Build().ToList();
            _resObject = Builder<ResponseObject>.CreateNew().Build();
            _resObject.Data = _batteryUsageList;
            _resObject.Message = "UnitTest";
        }

        [TestMethod]
        public async Task CalculateBatteryDailyAverage_Sucessfully()
        {
            _mockResponseHelper.Setup(x => x.SuccessResponse(It.IsAny<string>(), It.IsAny<object>())).Returns(_resObject);

            var response = await _batteryUsageLogics.CalculateBatteryDailyAverage(_deviceTrackPointList);

            Assert.IsNotNull(response);
            Assert.AreEqual(response.Message, "UnitTest");
        }
    }
}

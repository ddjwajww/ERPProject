using Microsoft.AspNetCore.Mvc;
using SASSTS.Business.Interfaces;
using SASSTS.Model.RequestModels.AccountingVM;
using SASSTS.Model.RequestModels.EmployeeVM;

namespace SASSTS.WebAPI.Controllers
{
    [Route("api/accountings")]
    [ApiController]
    public class AccountingController : BaseController
    {
        private readonly IAccountingBs _accountingBs;
        public AccountingController(IAccountingBs accountingBs) => _accountingBs = accountingBs;



        [HttpGet("getaccountbyAccountId")]
        public async Task<IActionResult> GetbyAccountId([FromQuery] int accountingId) => 
            SendResponse(await _accountingBs.GetAccountbyAccountingId(accountingId, "Employee"));



        [HttpGet("getAccountings")]
        public async Task<IActionResult> GetAccounting() => SendResponse(await _accountingBs.GetAccounting("Employee"));

        [HttpPost("loginTwoFactor")]
        public async Task<IActionResult> LoginTwoFactor([FromBody] LoginVM loginVM) => SendResponse(await _accountingBs.LoginTwoFactor(loginVM));

        [HttpPost("randomNumberLogin")]
        public async Task<IActionResult> RandomNumberLogin([FromBody] RandomNumberNoVM randomNumberNoVM) => SendResponse(await _accountingBs.RandomNumberLogin(randomNumberNoVM));

        [HttpPost("forgottenPassword")]
        public async Task<IActionResult> ForgottenPassword([FromBody] EmployeeMailVM employeeMailVM) => SendResponse(await _accountingBs.ForgottenPassword(employeeMailVM));

        [HttpPost("RandomNumberConfirm")]
        public async Task<IActionResult> RandomNumberConfirm([FromBody] RandomNumberNoVM randomNumberNoVM) => SendResponse(await _accountingBs.RandomNumberConfirm(randomNumberNoVM));

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserVM updateUserVM) => SendResponse(await _accountingBs.UpdateUser(updateUserVM));

        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeVM updateEmployeeVM) => SendResponse(await _accountingBs.UpdateEmployee(updateEmployeeVM));

        [HttpPost("NewPassword")]
        public async Task<IActionResult> NewPassword([FromBody] NewPasswordVM newPasswordVM) => SendResponse(await _accountingBs.NewPassword(newPasswordVM));

        [HttpPost("DeleteRandomNumber")]
        public async Task<IActionResult> DeleteRandomNumber([FromBody] NewPasswordVM newPasswordVM) => SendResponse(await _accountingBs.DeleteRandomNumber(newPasswordVM));

        [HttpPost("DeleteRandomNumberLogin")]
        public async Task<IActionResult> DeleteRandomNumberLogin([FromBody] RandomNumberNoVM randomNumberNoVM) => SendResponse(await _accountingBs.DeleteRandomNumber(randomNumberNoVM));
    }
}
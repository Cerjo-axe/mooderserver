using DTO;

namespace Mooder.UnitTests.Helpers;

public static class DummyData
{
    public static RegisterDTO validUser1 = new RegisterDTO(){
                                            UserName="Name8carac",
                                            Email="valid@gmail.com",
                                            Password="Teste#1234",
                                            ConfirmPassword="Teste#1234"
                                            };
    public static RegisterDTO invalidUser1 = new RegisterDTO(){
                                UserName="Teste",
                                Email="emailinvalido",
                                Password="senhai",
                                ConfirmPassword="senhai"
                                                        };
}

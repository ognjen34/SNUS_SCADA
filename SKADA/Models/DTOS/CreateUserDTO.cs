namespace SKADA.Models.DTOS
{
    public class CreateUserDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }

        public List<String> AnalogInputsIds { get; set; }

        public List<String> DigitalInputsIds { get; set; }
    }
}

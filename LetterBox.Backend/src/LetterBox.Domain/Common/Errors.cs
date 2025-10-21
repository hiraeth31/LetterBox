namespace LetterBox.Domain.Common
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} запись не найдена");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" для Id {id}";
                return Error.NotFound("record.not.found", $"Запись не найдена для{forId}");
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var label = name == null ? " " : $" {name} ";
                return Error.NotFound("vakue.is.required", $"Поле{name}обязательно к заполнению");
            }

            public static Error AlreadyExist()
            {
                return Error.Validation("record.already.exist", $"Запись уже существует");
            }
        }
    }
}

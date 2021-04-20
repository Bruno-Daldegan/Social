using Social.Model.Modelos;

namespace Social.Model.Extensoes
{
    public static class ContaExtensions
    {
        public static Conta ToConta(this ContaApi model)
        {
            return new Conta
            {
                ContaId = model.Idenfifier,
                Nome = model.Name,
                Descricao = model.Description,
                Status = model.Status.Equals("ACTIVE")
            };
        }

        public static ContaApi ToApi(this Conta conta)
        {
            return new ContaApi
            {
                Idenfifier = conta.ContaId,
                Name = conta.Nome,
                Description = conta.Descricao,
                Status = conta.Status ? "ACTIVE" : "INATIVE"
            };
        }

    }

}

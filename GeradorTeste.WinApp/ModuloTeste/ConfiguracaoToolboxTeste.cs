namespace GeradorTeste.WinApp.ModuloTeste
{
    public class ConfiguracaoToolboxTeste : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Criação de Testes";

        public override string TooltipInserir => "Novo Teste";

        public override string TooltipEditar => "";

        public override string TooltipExcluir => "Excluir um Teste existente";

        public override string TooltipGerarPdf => "Gerar PDF do Teste";

        public override string TooltipDuplicar => "Duplicar o Teste selecionado";

        public override bool GerarPdfHabilitado => true;

        
    }
}

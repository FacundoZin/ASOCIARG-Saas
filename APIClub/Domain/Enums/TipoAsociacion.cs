using System.ComponentModel;

namespace APIClub.Domain.Enums
{
    public enum TipoAsociacion
    {
        Ninguna = 0,

        [Description("Club deportivo")]
        Deportiva = 1,

        [Description("Asociación cultural")]
        Cultural = 2,

        [Description("Asociación social")]
        Social = 3,

        [Description("Centro vecinal")]
        Vecinal = 4,

        [Description("Asociación educativa")]
        Educativa = 5,

        [Description("Asociación recreativa")]
        Recreativa = 6,

        [Description("Asociación benéfica")]
        Benefica = 7,

        [Description("Asociación comunitaria")]
        Comunitaria = 8,

        [Description("Centro de jubilados")]
        CentroDeJubilados = 9,

        [Description("Biblioteca popular")]
        BibliotecaPopular = 10,

        [Description("Cooperadora")]
        Cooperadora = 11,

        [Description("Otra actividad")]
        Otra = 99
    }
}

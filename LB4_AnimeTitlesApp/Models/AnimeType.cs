using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LB4_AnimeTitlesApp.Models;

// класс модели соответствует таблице в базе данных
// экземпляр класса - это строка таблицы базы данных
public partial class AnimeType
{
    // свойства класса соответствуют полям таблицы в базе данных
    public short Id { get; set; }

    public string AnimeOfType { get; set; } = null!;

    // навигационное свойство
    // помогает связывать таблицы в коде
    // одному типу аниме соответствует много тайтлов аниме,
    // т.е. создается коллекция экземпляров AnimeTitle
    public virtual ICollection<AnimeTitle> AnimeTitles { get; set; } = new List<AnimeTitle>();
}

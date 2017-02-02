using System;

namespace Musicly.Core.Dtos
{
    public class GigDto
    {
        public GigDto(bool isCancel)
        {
            IsCancel = isCancel;
        }

        public int Id { get; set; }
        public bool IsCancel { get; private set; }
        public UserDto Artist { get; set; }
        public DateTime DateTime { get; set; }

        public string Venue { get; set; }
        public GenreDto Genre { get; set; }

    }
}
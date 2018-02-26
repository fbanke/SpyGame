using System;
namespace SpyLib
{
    public class Board
    {
        private int[] config;
        private BoardValidator validator;

        public Board(int[] config, BoardValidator validator)
        {
            this.config = config;
            this.validator = validator;
        }

        public bool IsValid()
        {
            return validator.IsValid(config);
        }
    }
}

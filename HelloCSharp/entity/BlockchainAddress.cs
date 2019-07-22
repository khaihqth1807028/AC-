namespace HelloCSharp.entity
{
    // có thể là một sàn nào đó.
    public class BlockchainAddress
    {
        public string Address { get; set; }
        public string PrivateKey { get; set; }
        public double Balance { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }

        public BlockchainAddress()
        {
            
        }
    }
}
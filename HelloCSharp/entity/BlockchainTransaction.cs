namespace HelloCSharp.entity
{
    public class BlockchainTransaction
    {
        public string TransactionId { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverAddress { get; set; }
        public double Amount { get; set; }
        public long CreatedAtMLS { get; set; }
        public long UpdatedAtMLS { get; set; }
        public int Status { get; set; }
        public int Type { get; set; }

        public BlockchainTransaction()
        {
        }

        public BlockchainTransaction(string senderAddress, string receiverAddress, double amount)
        {
            SenderAddress = senderAddress;
            ReceiverAddress = receiverAddress;
            Amount = amount;
        }
    }
}
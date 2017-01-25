CREATE TRIGGER bid_validation
	ON [dbo].[Bids]
	FOR INSERT
	AS
	BEGIN
	IF @@ROWCOUNT=1
	BEGIN
		SET NOCOUNT OFF
		DECLARE @auctPrice money, 
		@bidPrice money, 
		@userBidId int, 
		@lastUserBidId int, 
		@targetAuctionId int,
		@bidDT datetime2,
		@finalDT datetime2

		SELECT @userBidId = (SELECT TOP 1 UserId FROM inserted)
		SELECT @targetAuctionId = (SELECT TOP 1 AuctionId FROM inserted)
		SELECT @bidPrice = (SELECT TOP 1 Amount FROM inserted)

		SELECT @auctPrice = (SELECT TOP 1 Price FROM Auctions WHERE AuctionId = @targetAuctionId)
		SELECT @lastUserBidId = (SELECT TOP 1 UserId FROM Bids WHERE AuctionId = @targetAuctionId ORDER BY Bids.Datetime DESC)

		SELECT @bidDT = (SELECT TOP 1 Datetime FROM inserted)
		SELECT @finalDT = (SELECT FinishDate FROM Auctions WHERE AuctionId = @targetAuctionId)

		IF @bidPrice < @auctPrice OR @userBidId = @lastUserBidId OR @bidDT > @finalDT
		BEGIN
		ROLLBACK TRANSACTION
		END
		ELSE UPDATE Auctions SET Price=@bidPrice WHERE AuctionId = @targetAuctionId
		END
	END

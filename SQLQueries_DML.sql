USE [ProDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_addProduct]    Script Date: 7/31/2023 9:08:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_addProduct] (
@Name varchar(50),
@Description varchar(500),
@Price int,
@Id int output
)
As
Begin
INSERT INTO [dbo].[Products]
           ([Name]
           ,[Description]
           ,[Price]
           )
     VALUES(@Name,@Description,@Price)
	 SET @id=SCOPE_IDENTITY()
    RETURN  @id
End      
GO


USE [ProDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_deleteProduct]    Script Date: 7/31/2023 9:09:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_deleteProduct](@Id int)
As
Begin
DELETE FROM [dbo].[Products]
      WHERE Id=@Id
	  RETURN  1
End
GO


USE [ProDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_updateProduct]    Script Date: 7/31/2023 9:09:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_updateProduct](
@Id Int,
@Name varchar(50),
@Description varchar(500),
@Price int,
@Outputid int output)
As
Begin

UPDATE [dbo].[Products]
   SET [Name] = @Name
      ,[Description] = @Description
      ,[Price] = @Price
 WHERE Id=@Id
  
    RETURN  @outputid
 End
GO


USE [ProDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_getAllProducts]    Script Date: 7/31/2023 9:10:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_getAllProducts]
AS
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[Price]
      --,[Image]
  FROM [dbo].[Products] Order By Id
GO


USE [ProDB]
GO

/****** Object:  StoredProcedure [dbo].[usp_getProductById]    Script Date: 7/31/2023 9:10:35 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_getProductById](@Id AS Int)
AS
SELECT [Id]
      ,[Name]
      ,[Description]
      ,[Price]
      --,[Image]
  FROM [dbo].[Products] where Id=@Id
GO



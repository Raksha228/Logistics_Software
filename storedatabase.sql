--Create database [Logistics_Software]

--DROP DATABASE [Logistics_Software]


USE [Logistics_Software]
GO
/****** Object:  Table [dbo].[table_categories]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](50) NULL,
	[description] [text] NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_categories] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_dealer_customer]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_dealer_customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NULL,
	[name] [varchar](150) NULL,
	[email] [varchar](150) NULL,
	[contact] [varchar](15) NULL,
	[address] [text] NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_dealer_customer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_logistic]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_logistic](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee] [varchar](50) NULL,
	[first_name_employee] [varchar](50) NULL,
	[last_name_employee] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[contact] [varchar](50) NULL,
	[date] [varchar](50) NULL,
	[description] [text] NULL,
	[price] [decimal](18, 2) NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_logistic] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_logistic_archive]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_logistic_archive](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[employee] [varchar](50) NULL,
	[first_name_employee] [varchar](50) NULL,
	[last_name_employee] [varchar](50) NULL,
	[address] [varchar](50) NULL,
	[contact] [varchar](50) NULL,
	[date] [varchar](50) NULL,
	[description] [text] NULL,
	[price] [decimal](18, 2) NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_logistic_archive_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_products]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NULL,
	[category] [varchar](50) NULL,
	[special_number] [varchar](50) NULL,
	[description] [text] NULL,
	[rate] [decimal](18, 2) NULL,
	[qty] [decimal](18, 2) NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_transactions]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_transactions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [varchar](50) NULL,
	[dea_cust_id] [int] NULL,
	[description] [text] NULL,
	[grandTotal] [decimal](18, 2) NULL,
	[transaction_date] [datetime] NULL,
	[tax] [decimal](18, 2) NULL,
	[discount] [decimal](18, 2) NULL,
	[paid_amount] [decimal](18, 2) NULL,
	[return_amount] [decimal](18, 2) NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_transactions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_transactions_detail]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_transactions_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NULL,
	[rate] [decimal](18, 2) NULL,
	[qty] [decimal](18, 2) NULL,
	[total] [decimal](18, 2) NULL,
	[dea_cust_id] [int] NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_transactions_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_users]    Script Date: 29.5.2025 г. 20:25:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](50) NULL,
	[last_name] [varchar](50) NULL,
	[email] [varchar](150) NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](15) NULL,
	[contact] [varchar](50) NULL,
	[address] [text] NULL,
	[gender] [varchar](10) NULL,
	[user_type] [varchar](15) NULL,
	[added_date] [datetime] NULL,
	[added_by] [int] NULL,
	[added_by_name] [varchar](50) NULL,
 CONSTRAINT [PK_table_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


SET IDENTITY_INSERT [dbo].[table_categories] ON

INSERT [dbo].[table_categories] ([id], [title], [description], [added_date], [added_by], [added_by_name]) VALUES (3, N'Еда', N'Что-то поесть', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_categories] ([id], [title], [description], [added_date], [added_by], [added_by_name]) VALUES (4, N'Напиток', N'Что-то попить', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_categories] ([id], [title], [description], [added_date], [added_by], [added_by_name]) VALUES (5, N'Предмет', N'Что-то для дома', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_categories] ([id], [title], [description], [added_date], [added_by], [added_by_name]) VALUES (6, N'Инструмент', N'Что-то для использования', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
SET IDENTITY_INSERT [dbo].[table_categories] OFF
GO

SET IDENTITY_INSERT [dbo].[table_dealer_customer] ON

INSERT [dbo].[table_dealer_customer] ([id], [type], [name], [email], [contact], [address], [added_date], [added_by], [added_by_name]) VALUES (3, N'Поставщик', N'Tolga', N'tolgaferad@gmail.com', N'0887565412', N'ул. София Град', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, NULL)
INSERT [dbo].[table_dealer_customer] ([id], [type], [name], [email], [contact], [address], [added_date], [added_by], [added_by_name]) VALUES (4, N'Клиент', N'Teodor', N'teodornikolov@gmail.com', N'0876248351', N'ул. София Град', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, NULL)
SET IDENTITY_INSERT [dbo].[table_dealer_customer] OFF
GO

SET IDENTITY_INSERT [dbo].[table_logistic] ON

INSERT [dbo].[table_logistic] ([id], [employee], [first_name_employee], [last_name_employee], [address], [contact], [date], [description], [price], [added_date], [added_by], [added_by_name]) VALUES (23, N'admin', N'Мастер', N'Админ', N'АУЕ', N'1515151515', N'29.05.2025', N'Чипсы=2 ', CAST(13.38 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
SET IDENTITY_INSERT [dbo].[table_logistic] OFF
GO

SET IDENTITY_INSERT [dbo].[table_logistic_archive] ON

INSERT [dbo].[table_logistic_archive] ([id], [employee], [first_name_employee], [last_name_employee], [address], [contact], [date], [description], [price], [added_date], [added_by], [added_by_name]) VALUES (1, N'user', N'Обычный', N'Пользователь', N'Джо Байден', N'52515145152', N'29.05.2025', N'Чипсы=2 ', CAST(13.38 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
SET IDENTITY_INSERT [dbo].[table_logistic_archive] OFF
GO

SET IDENTITY_INSERT [dbo].[table_products] ON

INSERT [dbo].[table_products] ([id], [name], [category], [special_number], [description], [rate], [qty], [added_date], [added_by], [added_by_name]) VALUES (4, N'Чипсы', N'Еда', N'111', N'Жареный картофель.', CAST(2.69 AS Decimal(18, 2)), CAST(16.00 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_products] ([id], [name], [category], [special_number], [description], [rate], [qty], [added_date], [added_by], [added_by_name]) VALUES (5, N'Желе', N'Еда', N'112', N'Жевательные конфеты.', CAST(2.20 AS Decimal(18, 2)), CAST(8.00 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_products] ([id], [name], [category], [special_number], [description], [rate], [qty], [added_date], [added_by], [added_by_name]) VALUES (6, N'Кока-Кола', N'Напиток', N'211', N'Газировка.', CAST(2.50 AS Decimal(18, 2)), CAST(14.00 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_products] ([id], [name], [category], [special_number], [description], [rate], [qty], [added_date], [added_by], [added_by_name]) VALUES (7, N'Кор', N'Напиток', N'212', N'Вода.', CAST(1.70 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_products] ([id], [name], [category], [special_number], [description], [rate], [qty], [added_date], [added_by], [added_by_name]) VALUES (8, N'Спрайт', N'Напиток', N'213', N'Газировка.', CAST(2.50 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
SET IDENTITY_INSERT [dbo].[table_products] OFF
GO

SET IDENTITY_INSERT [dbo].[table_transactions] ON

INSERT [dbo].[table_transactions] ([id], [type], [dea_cust_id], [description], [grandTotal], [transaction_date], [tax], [discount], [paid_amount], [return_amount], [added_by], [added_by_name]) VALUES (11, N'Закупка', 0, N'Кор=4 Желе=4 ', CAST(14.81 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), CAST(1.00 AS Decimal(18, 2)), CAST(6.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(0.19 AS Decimal(18, 2)), 6, NULL)
INSERT [dbo].[table_transactions] ([id], [type], [dea_cust_id], [description], [grandTotal], [transaction_date], [tax], [discount], [paid_amount], [return_amount], [added_by], [added_by_name]) VALUES (12, N'Закупка', 0, N'Кока-Кола=7 ', CAST(16.06 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), CAST(2.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(17.00 AS Decimal(18, 2)), CAST(0.94 AS Decimal(18, 2)), 6, NULL)
INSERT [dbo].[table_transactions] ([id], [type], [dea_cust_id], [description], [grandTotal], [transaction_date], [tax], [discount], [paid_amount], [return_amount], [added_by], [added_by_name]) VALUES (13, N'Продажа', 0, N'Чипсы=5 ', CAST(12.91 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), CAST(1.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(15.00 AS Decimal(18, 2)), CAST(2.09 AS Decimal(18, 2)), 6, NULL)
INSERT [dbo].[table_transactions] ([id], [type], [dea_cust_id], [description], [grandTotal], [transaction_date], [tax], [discount], [paid_amount], [return_amount], [added_by], [added_by_name]) VALUES (14, N'Закупка', 0, N'Чипсы=20 ', CAST(51.62 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), CAST(1.00 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(48.38 AS Decimal(18, 2)), 6, NULL)
INSERT [dbo].[table_transactions] ([id], [type], [dea_cust_id], [description], [grandTotal], [transaction_date], [tax], [discount], [paid_amount], [return_amount], [added_by], [added_by_name]) VALUES (18, N'Закупка', 0, N'Чипсы=5 ', CAST(12.34 AS Decimal(18, 2)), CAST(N'2025-05-29T20:26:08.233' AS DateTime), CAST(2.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(13.00 AS Decimal(18, 2)), CAST(0.66 AS Decimal(18, 2)), 6, N'user')
SET IDENTITY_INSERT [dbo].[table_transactions] OFF
GO

SET IDENTITY_INSERT [dbo].[table_transactions_detail] ON

INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (1, 2, CAST(20.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (2, 2, CAST(20.00 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (3, 2, CAST(20.00 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(200.00 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (4, 3, CAST(2.69 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(26.90 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (5, 4, CAST(2.69 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(26.90 AS Decimal(18, 2)), 3, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (6, 5, CAST(2.20 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(22.00 AS Decimal(18, 2)), 3, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (7, 6, CAST(2.50 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), 3, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (8, 7, CAST(1.70 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(17.00 AS Decimal(18, 2)), 3, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (9, 8, CAST(2.50 AS Decimal(18, 2)), CAST(10.00 AS Decimal(18, 2)), CAST(25.00 AS Decimal(18, 2)), 3, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (10, 6, CAST(2.50 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(12.50 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (11, 4, CAST(2.69 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(5.38 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (12, 6, CAST(2.50 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), CAST(7.50 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (13, 4, CAST(2.69 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(5.38 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (14, 6, CAST(2.50 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), CAST(7.50 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (15, 4, CAST(2.69 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(5.38 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (16, 5, CAST(2.20 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(4.40 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (17, 7, CAST(1.70 AS Decimal(18, 2)), CAST(2.00 AS Decimal(18, 2)), CAST(3.40 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (18, 4, CAST(2.69 AS Decimal(18, 2)), CAST(1.00 AS Decimal(18, 2)), CAST(2.69 AS Decimal(18, 2)), 4, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (19, 4, CAST(2.69 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(18.83 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (20, 7, CAST(1.70 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(6.80 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (21, 5, CAST(2.20 AS Decimal(18, 2)), CAST(4.00 AS Decimal(18, 2)), CAST(8.80 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (22, 6, CAST(2.50 AS Decimal(18, 2)), CAST(7.00 AS Decimal(18, 2)), CAST(17.50 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (23, 4, CAST(2.69 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(13.45 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (24, 4, CAST(2.69 AS Decimal(18, 2)), CAST(20.00 AS Decimal(18, 2)), CAST(53.80 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, NULL)
INSERT [dbo].[table_transactions_detail] ([id], [product_id], [rate], [qty], [total], [dea_cust_id], [added_date], [added_by], [added_by_name]) VALUES (26, 4, CAST(2.69 AS Decimal(18, 2)), CAST(5.00 AS Decimal(18, 2)), CAST(13.45 AS Decimal(18, 2)), 0, CAST(N'2025-05-29T20:26:08.233' AS DateTime), 6, N'user')
SET IDENTITY_INSERT [dbo].[table_transactions_detail] OFF
GO

SET IDENTITY_INSERT [dbo].[table_users] ON

INSERT [dbo].[table_users] ([id], [first_name], [last_name], [email], [username], [password], [contact], [address], [gender], [user_type], [added_date], [added_by], [added_by_name]) VALUES (5, N'Мастер', N'admin', N'admin@gmail.com', N'admin', N'admin', N'01234567891', N'София, Россия', N'Мужчина', N'admin', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
INSERT [dbo].[table_users] ([id], [first_name], [last_name], [email], [username], [password], [contact], [address], [gender], [user_type], [added_date], [added_by], [added_by_name]) VALUES (6, N'Обычный', N'user', N'user@gmail.com', N'user', N'user', N'0123456789', N'София, Россия', N'Мужчина', N'user', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 1, NULL)
INSERT [dbo].[table_users] ([id], [first_name], [last_name], [email], [username], [password], [contact], [address], [gender], [user_type], [added_date], [added_by], [added_by_name]) VALUES (10, N'Георги', N'Анастасов', N'georgianastasov@gmail.com', N'georgianastasov', N'123456789', N'08844556789', N'ул. София Град', N'Мужчина', N'Пользователь', CAST(N'2025-05-29T20:26:08.233' AS DateTime), 5, N'admin')
SET IDENTITY_INSERT [dbo].[table_users] OFF
GO

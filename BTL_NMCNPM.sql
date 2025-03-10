/*
 Navicat Premium Dump SQL

 Source Server         : local_SQL
 Source Server Type    : SQL Server
 Source Server Version : 16001000 (16.00.1000)
 Source Host           : LAPTOP-4IAE44B1\SQLEXPRESS:1433
 Source Catalog        : NhapMonCNPM
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 16001000 (16.00.1000)
 File Encoding         : 65001

 Date: 10/03/2025 15:29:50
*/


-- ----------------------------
-- Table structure for tbl_Theloai
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Theloai]') AND type IN ('U'))
	DROP TABLE [dbo].[tbl_Theloai]
GO

CREATE TABLE [dbo].[tbl_Theloai] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [tentheloai] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [deleted] bit DEFAULT 0 NULL
)
GO

ALTER TABLE [dbo].[tbl_Theloai] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tbl_Theloai
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tbl_Theloai] ON
GO

INSERT INTO [dbo].[tbl_Theloai] ([id], [tentheloai], [deleted]) VALUES (N'1', N'Thể loại 1', N'0')
GO

INSERT INTO [dbo].[tbl_Theloai] ([id], [tentheloai], [deleted]) VALUES (N'2', N'Thể loại 2', N'0')
GO

INSERT INTO [dbo].[tbl_Theloai] ([id], [tentheloai], [deleted]) VALUES (N'3', N'Thể loại 3', N'0')
GO

INSERT INTO [dbo].[tbl_Theloai] ([id], [tentheloai], [deleted]) VALUES (N'4', N'Thể loại 4', N'0')
GO

INSERT INTO [dbo].[tbl_Theloai] ([id], [tentheloai], [deleted]) VALUES (N'5', N'Thể loại 5', N'0')
GO

INSERT INTO [dbo].[tbl_Theloai] ([id], [tentheloai], [deleted]) VALUES (N'6', N'Thể loại 6', N'0')
GO

SET IDENTITY_INSERT [dbo].[tbl_Theloai] OFF
GO


-- ----------------------------
-- Table structure for tbl_Taikhoan
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Taikhoan]') AND type IN ('U'))
	DROP TABLE [dbo].[tbl_Taikhoan]
GO

CREATE TABLE [dbo].[tbl_Taikhoan] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [tentaikhoan] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [matkhau] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [email] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [quyen] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [deleted] bit DEFAULT 0 NULL
)
GO

ALTER TABLE [dbo].[tbl_Taikhoan] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tbl_Taikhoan
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tbl_Taikhoan] ON
GO

INSERT INTO [dbo].[tbl_Taikhoan] ([id], [tentaikhoan], [matkhau], [email], [quyen], [deleted]) VALUES (N'2', N'admin', N'123', N'ex@gmail.com', N'admin', N'0')
GO

INSERT INTO [dbo].[tbl_Taikhoan] ([id], [tentaikhoan], [matkhau], [email], [quyen], [deleted]) VALUES (N'3', N'tesst', N'123', N't@gmail.com', N'casi', N'0')
GO

SET IDENTITY_INSERT [dbo].[tbl_Taikhoan] OFF
GO


-- ----------------------------
-- Table structure for tbl_Playlist_baihat
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Playlist_baihat]') AND type IN ('U'))
	DROP TABLE [dbo].[tbl_Playlist_baihat]
GO

CREATE TABLE [dbo].[tbl_Playlist_baihat] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [FK_Playlist] int  NULL,
  [FK_Baihat] int  NULL,
  [deleted] bit  NULL
)
GO

ALTER TABLE [dbo].[tbl_Playlist_baihat] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tbl_Playlist_baihat
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tbl_Playlist_baihat] ON
GO

SET IDENTITY_INSERT [dbo].[tbl_Playlist_baihat] OFF
GO


-- ----------------------------
-- Table structure for tbl_Playlist
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Playlist]') AND type IN ('U'))
	DROP TABLE [dbo].[tbl_Playlist]
GO

CREATE TABLE [dbo].[tbl_Playlist] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [tenPlaylist] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [deleted] bit DEFAULT 0 NULL,
  [FK_nguoitao] int  NULL
)
GO

ALTER TABLE [dbo].[tbl_Playlist] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tbl_Playlist
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tbl_Playlist] ON
GO

INSERT INTO [dbo].[tbl_Playlist] ([id], [tenPlaylist], [deleted], [FK_nguoitao]) VALUES (N'1', N'Test Playlist', N'0', N'2')
GO

INSERT INTO [dbo].[tbl_Playlist] ([id], [tenPlaylist], [deleted], [FK_nguoitao]) VALUES (N'2', N'aaa', N'0', N'3')
GO

INSERT INTO [dbo].[tbl_Playlist] ([id], [tenPlaylist], [deleted], [FK_nguoitao]) VALUES (N'3', N'bbb', N'0', N'3')
GO

SET IDENTITY_INSERT [dbo].[tbl_Playlist] OFF
GO


-- ----------------------------
-- Table structure for tbl_Binhluan
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Binhluan]') AND type IN ('U'))
	DROP TABLE [dbo].[tbl_Binhluan]
GO

CREATE TABLE [dbo].[tbl_Binhluan] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [noidung] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [FK_nguoiviet] int  NOT NULL,
  [thoigian] datetime DEFAULT getdate() NULL,
  [FK_parent] int  NULL,
  [deleted] bit  NULL
)
GO

ALTER TABLE [dbo].[tbl_Binhluan] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tbl_Binhluan
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tbl_Binhluan] ON
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'1', N'Testtt', N'3', N'2025-03-10 15:07:35.837', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'2', N'Đây là bình luận', N'2', N'2025-03-10 15:14:30.533', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'3', N'Test binh luan 2', N'2', N'2025-03-10 15:15:31.870', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'4', N'aaa', N'2', N'2025-03-10 15:15:40.367', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'5', N'Test bl 3', N'2', N'2025-03-10 15:15:58.730', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'6', N'bbb', N'2', N'2025-03-10 15:16:44.330', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'7', N'sss', N'2', N'2025-03-10 15:19:49.360', N'2', N'0')
GO

INSERT INTO [dbo].[tbl_Binhluan] ([id], [noidung], [FK_nguoiviet], [thoigian], [FK_parent], [deleted]) VALUES (N'8', N'hhh', N'2', N'2025-03-10 15:21:09.103', N'2', N'0')
GO

SET IDENTITY_INSERT [dbo].[tbl_Binhluan] OFF
GO


-- ----------------------------
-- Table structure for tbl_Baihat
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_Baihat]') AND type IN ('U'))
	DROP TABLE [dbo].[tbl_Baihat]
GO

CREATE TABLE [dbo].[tbl_Baihat] (
  [id] int  IDENTITY(1,1) NOT NULL,
  [tenbaihat] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [FK_theloai] int  NOT NULL,
  [file] nvarchar(255) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [thoigian_up] datetime DEFAULT getdate() NULL,
  [FK_nguoiup] int  NULL,
  [deleted] bit DEFAULT 0 NULL,
  [mota] text COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[tbl_Baihat] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of tbl_Baihat
-- ----------------------------
SET IDENTITY_INSERT [dbo].[tbl_Baihat] ON
GO

INSERT INTO [dbo].[tbl_Baihat] ([id], [tenbaihat], [FK_theloai], [file], [thoigian_up], [FK_nguoiup], [deleted], [mota]) VALUES (N'2', N'Test', N'2', N'/uploads/5c90f4e7-b875-4e79-b8db-3333c0d3aab6_KHÔNG THỂ BUÔNG - JAPANDEE REMIX [ ezmp3.cc ].mp3', N'2025-03-08 16:53:51.020', N'3', N'0', NULL)
GO

INSERT INTO [dbo].[tbl_Baihat] ([id], [tenbaihat], [FK_theloai], [file], [thoigian_up], [FK_nguoiup], [deleted], [mota]) VALUES (N'3', N'RADWIMP', N'2', N'/uploads/d1f1acd1-bae2-4366-878b-c7b07b5e1008_RADWIMPS - すずめ feat.十明 [Official Lyric Video] [ ezmp3.cc ].mp3', N'2025-03-10 15:23:36.437', N'3', N'0', NULL)
GO

SET IDENTITY_INSERT [dbo].[tbl_Baihat] OFF
GO


-- ----------------------------
-- Auto increment value for tbl_Theloai
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tbl_Theloai]', RESEED, 7)
GO


-- ----------------------------
-- Primary Key structure for table tbl_Theloai
-- ----------------------------
ALTER TABLE [dbo].[tbl_Theloai] ADD CONSTRAINT [PK__tbl_Thel__3213E83FFFF6937C] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tbl_Taikhoan
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tbl_Taikhoan]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table tbl_Taikhoan
-- ----------------------------
ALTER TABLE [dbo].[tbl_Taikhoan] ADD CONSTRAINT [PK__tbl_Taik__3213E83F5A442FE3] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tbl_Playlist_baihat
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tbl_Playlist_baihat]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table tbl_Playlist_baihat
-- ----------------------------
ALTER TABLE [dbo].[tbl_Playlist_baihat] ADD CONSTRAINT [PK__tbl_Play__3213E83FB7D83253] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tbl_Playlist
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tbl_Playlist]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table tbl_Playlist
-- ----------------------------
ALTER TABLE [dbo].[tbl_Playlist] ADD CONSTRAINT [PK__tbl_Play__3213E83F39C1B58A] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tbl_Binhluan
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tbl_Binhluan]', RESEED, 8)
GO


-- ----------------------------
-- Primary Key structure for table tbl_Binhluan
-- ----------------------------
ALTER TABLE [dbo].[tbl_Binhluan] ADD CONSTRAINT [PK__tbl_Binh__3213E83FC5482394] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for tbl_Baihat
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[tbl_Baihat]', RESEED, 3)
GO


-- ----------------------------
-- Primary Key structure for table tbl_Baihat
-- ----------------------------
ALTER TABLE [dbo].[tbl_Baihat] ADD CONSTRAINT [PK__tbl_Baih__3213E83FDCDDEADD] PRIMARY KEY CLUSTERED ([id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table tbl_Playlist
-- ----------------------------
ALTER TABLE [dbo].[tbl_Playlist] ADD CONSTRAINT [Fk_nguoitao_pl] FOREIGN KEY ([FK_nguoitao]) REFERENCES [dbo].[tbl_Taikhoan] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table tbl_Binhluan
-- ----------------------------
ALTER TABLE [dbo].[tbl_Binhluan] ADD CONSTRAINT [Fk_bl_bh] FOREIGN KEY ([FK_parent]) REFERENCES [dbo].[tbl_Baihat] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table tbl_Baihat
-- ----------------------------
ALTER TABLE [dbo].[tbl_Baihat] ADD CONSTRAINT [Fk_baihat_theloai] FOREIGN KEY ([FK_theloai]) REFERENCES [dbo].[tbl_Theloai] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


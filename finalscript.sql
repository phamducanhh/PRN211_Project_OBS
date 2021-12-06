USE [master]
GO
/****** Object:  Database [OnlineBookShop]    Script Date: 11/09/2021 20:39:18 ******/
CREATE DATABASE [OnlineBookShop] ON  PRIMARY 
( NAME = N'OnlineBookShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\OnlineBookShop.mdf' , SIZE = 2304KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OnlineBookShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\OnlineBookShop_log.LDF' , SIZE = 512KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OnlineBookShop] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineBookShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineBookShop] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [OnlineBookShop] SET ANSI_NULLS OFF
GO
ALTER DATABASE [OnlineBookShop] SET ANSI_PADDING OFF
GO
ALTER DATABASE [OnlineBookShop] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [OnlineBookShop] SET ARITHABORT OFF
GO
ALTER DATABASE [OnlineBookShop] SET AUTO_CLOSE ON
GO
ALTER DATABASE [OnlineBookShop] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [OnlineBookShop] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [OnlineBookShop] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [OnlineBookShop] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [OnlineBookShop] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [OnlineBookShop] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [OnlineBookShop] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [OnlineBookShop] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [OnlineBookShop] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [OnlineBookShop] SET  ENABLE_BROKER
GO
ALTER DATABASE [OnlineBookShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [OnlineBookShop] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [OnlineBookShop] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [OnlineBookShop] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [OnlineBookShop] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [OnlineBookShop] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [OnlineBookShop] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [OnlineBookShop] SET  READ_WRITE
GO
ALTER DATABASE [OnlineBookShop] SET RECOVERY SIMPLE
GO
ALTER DATABASE [OnlineBookShop] SET  MULTI_USER
GO
ALTER DATABASE [OnlineBookShop] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [OnlineBookShop] SET DB_CHAINING OFF
GO
USE [OnlineBookShop]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](200) NULL,
	[wiki_url] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Author] ON
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (1, N'Lewis Carroll', N'https://en.wikipedia.org/wiki/Lewis_Carroll')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (2, N'Alexandre Dumas', N'https://en.wikipedia.org/wiki/Alexandre_Dumas')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (3, N'Bram Stoker', N'https://en.wikipedia.org/wiki/Bram_Stoker')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (4, N'William Shakespeare', N'https://en.wikipedia.org/wiki/William_Shakespeare')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (5, N'Gaston Leroux', N'https://en.wikipedia.org/wiki/Gaston_Leroux')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (6, N'Stephen King', N'https://en.wikipedia.org/wiki/Stephen_King')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (7, N'John Berendt', N'https://en.wikipedia.org/wiki/John_Berendt')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (8, N'Diana Wynne Jones', N'https://en.wikipedia.org/wiki/Diana_Wynne_Jones')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (9, N'J. R. R. Tolkien', N'https://en.wikipedia.org/wiki/J._R._R._Tolkien')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (10, N'George R. R. Martin', N'https://en.wikipedia.org/wiki/George_R._R._Martin')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (11, N'Shi Nai''an', N'https://en.wikipedia.org/wiki/Shi_Nai%27an')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (12, N'Mario Puzo', N'https://en.wikipedia.org/wiki/Mario_Puzo')
INSERT [dbo].[Author] ([id], [name], [wiki_url]) VALUES (13, N'Agatha Christie', N'https://en.wikipedia.org/wiki/Agatha_Christie')
SET IDENTITY_INSERT [dbo].[Author] OFF
/****** Object:  Table [dbo].[Genre]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Genre] ON
INSERT [dbo].[Genre] ([id], [name]) VALUES (1, N'Children')
INSERT [dbo].[Genre] ([id], [name]) VALUES (2, N'Fiction')
INSERT [dbo].[Genre] ([id], [name]) VALUES (3, N'Non-Fic')
INSERT [dbo].[Genre] ([id], [name]) VALUES (4, N'Historical')
INSERT [dbo].[Genre] ([id], [name]) VALUES (5, N'Action')
INSERT [dbo].[Genre] ([id], [name]) VALUES (6, N'Mystery')
INSERT [dbo].[Genre] ([id], [name]) VALUES (7, N'Horror')
INSERT [dbo].[Genre] ([id], [name]) VALUES (8, N'Romance')
SET IDENTITY_INSERT [dbo].[Genre] OFF
/****** Object:  Table [dbo].[Role]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Role] ON
INSERT [dbo].[Role] ([id], [title]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([id], [title]) VALUES (2, N'Customer')
SET IDENTITY_INSERT [dbo].[Role] OFF
/****** Object:  Table [dbo].[User]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](100) NOT NULL,
	[password] [nvarchar](30) NOT NULL,
	[username] [nvarchar](100) NOT NULL,
	[avatar_url] [nvarchar](200) NOT NULL,
	[role_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([id], [email], [password], [username], [avatar_url], [role_id]) VALUES (1, N'abcd@gmail.com', N'12345678', N'duc anh', N'1', 2)
INSERT [dbo].[User] ([id], [email], [password], [username], [avatar_url], [role_id]) VALUES (2, N'anhpdhe150529@fpt.edu.vn', N'1234', N'Pham Duc Anh', N'1', 2)
INSERT [dbo].[User] ([id], [email], [password], [username], [avatar_url], [role_id]) VALUES (3, N'cuabom@gmail.com', N'123456789', N'Tuan', N'1', 2)
INSERT [dbo].[User] ([id], [email], [password], [username], [avatar_url], [role_id]) VALUES (4, N'vietlong@gmail.com', N'12345678', N'viet long', N'https://avatars.githubusercontent.com/u/17879520?v=4', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[Book]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[image_url] [nvarchar](200) NOT NULL,
	[author_id] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[price] [float] NOT NULL,
	[status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Book] ON
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (23, N'Alice''s Adventures in Wonderland', N'AAIWDL0001.png', 1, N'On an ordinary summer''s afternoon, Alice tumbles down a hole and an extraordinary adventure begins. In a strange world with even stranger characters, she meets a grinning cat and a rabbit with a pocket watch, joins a Mad Hatter''s Tea Party, and plays croquet with the Queen! Lost in this fantasy land, Alice finds herself growing more and more curious by the minute . . .With a brilliant introduction by Chris Riddell, Alice''s Adventures in Wonderland is one of twenty much-loved classic stories relaunched with gorgeous new covers. The book includes a behind-the-scenes journey, including an author profile, a guide to who''s who, activities and more.', 129, 0)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (24, N'The Three Musketeers', N'TTMKTR0002.png', 2, N'Alexandre Dumas’s enthralling novel, set during the reign of Louis XIII, is the tale of a poor Gascon of noble descent intent on joining the legendary King’s Musketeers. This fabulous edition of one of literature’s greatest adventures, strictly limited to 750 copies, is bound in goatskin leather blocked in black and gold foils and features a signed and numbered etching by Roman Pisarev.', 159, 0)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (25, N'Dracula', N'DRCULA0003.png', 3, N'There can be few novels as influential as Bram Stoker’s much-adapted vampire tale. For this, the ultimate collector’s edition, artist Angela Barrett has produced 15 intensely atmospheric colour plates, a set of elaborate hand-drawn borders, nine black-and-white tailpieces, and a pair of striking designs on the venous red leather binding and black cloth-covered slipcase.', 219, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (26, N'The Letterpress Othello', N'LTPOTH0004.png', 4, N'Shakespeare’s explosive tale of love, jealousy and betrayal follows the triumphs and eventual downfall of the noble General Othello – his love for Desdemona, his trust in the villainous Iago and his obsessive jealousy that leads to murder. The sheer visceral strength of this great tragedy has resonated through the centuries, ensuring it remains one of Shakespeare’s most popular plays.', 439, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (27, N'Phantom of the Opera', N'PTMOPR0005.png', 5, N'Mysterious goings-on at the Paris opera house are brushed aside by its new owners, but the old-timers know that the resident ghost is more than an urban legend. One of the greatest gothic horror novels ever written, Gaston Leroux’s atmospheric adventure still chills first-time readers, then lures them back time and time again. Long eclipsed by the huge popularity of its musical adaptation, the book once again takes centre stage in this lavish new edition that drips with macabre imagery and theatrical allusion.', 349, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (28, N'Misery', N'MISERY0006.png', 6, N'Author Paul Sheldon regains consciousness in a stranger’s guest room, his legs shattered and useless. As the intense pain ebbs and flows, rare moments of lucidity lead to the chilling realisation that his rescuer is also his jailer. Annie Wilkes might be Paul’s ‘number one fan’, but she is incensed that he has killed the heroine Misery Chastain in his latest novel. Fate has given Annie a chance to bring her favourite character back to life, and Paul won’t be leaving Annie’s remote farmstead alive until he complies. Misery sees Stephen King at the height of his writing powers in a novel that explores the psyche of an author suffering extreme torment. Edward Kinsella illustrated the best-selling Folio edition of The Shining and he again immerses himself in King’s narrative to create a terrifying and compelling set of illustrations, as well as a binding design that defines the novel: the antique Royal typewriter that is both Paul Sheldon’s entrapment and his only hope of liberty.', 359, 0)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (29, N'Midnight in the Garden of Good and Evil', N'MNGNGE0007.png', 7, N'The ‘Bird Girl’ image, a photograph of the bronze statue that once stood in Bonaventure cemetery, now adorns the binding of this new edition. Jack Leigh’s iconic photograph featured on the 1994 first edition dust jacket and is synonymous with the book. More of Leigh’s original images of Savannah have been beautifully reproduced alongside seven further atmospheric photographs by Georgian photographers. Like courtroom evidence, the series of ten atmospheric photographs offer tantalising snapshots of the Old South and the city that captured the imagination of millions.', 239, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (30, N'The Shining', N'THSHNG0008.png', 6, N'Jack Torrance is the new winter caretaker of the Overlook, a grand hotel nestled in the isolated Colorado mountains and cut off from civilisation during the harsh cold months. Here, with his wife Wendy and their son Danny, Jack attempts to escape the mistakes of his past and rebuild a life with his family. But the hotel has other ideas. Using Danny’s strange precognitive gift – his ’hining’ – the evil that lurks inside the Overlook begins to stir, and take hold …', 359, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (31, N'Castle in the Air', N'CSTLAI0009.png', 8, N'Diana Wynne Jones’s sequel to the much-loved Howl’s Moving Castle explores more of her gorgeously imaginative world, taking the story to a distant land infused with the flavours of The Arabian Nights. Castle in the Air tells the sort of fairy tale only Jones could – warm, clever and enriched with humour, like gold silk in a magical carpet. Marie-Alice Harel, winner of the ninth Book Illustration Competition, returns with more of her exquisite artwork, including six gorgeous full colour images, black-and-white chapter head illustrations, and a wraparound binding design that floats the impossible castle of the title across a night’s sky of silvery stars. This beautiful edition of Castle in the Air also features delicately glittering endpapers depicting the gardens of Abdullah’s dreams, and a splendid decorated slipcase.', 59, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (32, N'Howl''s Moving Castle', N'HWMVCS0010.png', 8, N'A magical union of make-believe and reality, this much-loved young-adult fantasy spirits the reader off to faraway lands with an evil witch, a dashing wizard and an adventurous teenage girl. Folio’s charming new edition celebrates master storyteller Diana Wynne Jones’s creativity, alongside that of Folio’s 2019 Book Illustration Competition winner. Selected from a record-breaking 500 entries from around the world, Marie-Alice Harel re-enchants the fairy-tale tradition with her series of six images created with a lilac-themed palate. Harel also introduces the book’ 21 chapters with delicate black-and-white decorations that offer tantalising clues to the story, while the binding shows heroine Sophie Hatter being magnetically drawn to the gloomy castle of the title, with its mysterious four-fold aspect.', 59, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (33, N'The Hobbit', N'HOBBIT0011.png', 9, N'Bilbo Baggins is a reasonably typical hobbit: fond of sleeping, eating, drinking, parties and presents. However, it is his destiny to travel to the dwarflands in the east, to help slay the dragon Smaug. His quest takes him through enchanted forests, spiders’ lairs, and under the Misty Mountains, where he comes across the vile Gollum, and tricks him out of his ’Precious’ - a ring that makes its bearer invisible, and wields a terrible power of its own.', 259, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (34, N'The Lord of the Rings', N'LRDRNG0012.png', 9, N'The original ‘fantasy’ series, and still the greatest, The Lord of the Rings has sold over 100 million copies, been translated into more than 40 languages, and has been voted the best book of the 20th century. If there is any work of fiction that deserves to be owned in a magnificent edition – this is surely it.', 599, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (35, N'The Silmarillion', N'SLMRLN0013.png', 9, N'J. R. R. Tolkien began creating the mythology, traditions and language of Middle-earth long before The Lord of the Rings and The Hobbit ever found their way onto the printed page. Known as The Silmarillion, a book Tolkien would add to throughout his life, this rich tapestry of tales told of the creation of the world in the First Age, the coming of Elves and Men, the theft of the Silmarils – the jewels containing the pure light that illuminated Middle-earth – and the wars between the first Dark Lord and the High Elves.', 259, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (37, N'A Storm of Swords', N'ASTMSW0015.png', 10, N'The bloodiest, boldest and most addictive saga in fantasy continues with A Storm of Swords, the third volume in the ground-breaking ‘A Song of Ice and Fire’ series. Taking the world by storm, George R. R. Martin’s fantasy epic has won millions of fans worldwide, and these Folio editions – described by the author as ‘masterpieces of the bookmaker’ art’ – are packed full of details that devoted readers will find irresistible. Featuring exquisite illustrations by series artist Jonathan Burton, updated genealogies, a pair of bindings blocked in red and gold foils and specially designed chapter headings, this edition of A Storm of Swords exemplifies everything that makes a Folio edition so coveted.', 259, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (38, N'Outlaws of the Marsh', N'OLWMSH0016.png', 11, N'Originally related by oral tradition, Outlaws of the Marsh was first transcribed in the 14th century and is the earliest of the four Great Classics of Chinese Literature. From supernatural feats of endurance to tales of poisoning, witchcraft and cannibalism, the timeless tales of villainy and heroism have engrossed whole generations and still provide inspiration for comic books, films, Peking opera and computer games.', 799, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (39, N'The Godfather', N'GDFTHR0017.png', 12, N'One of the best-selling books of all time, it is almost impossible to exaggerate the influence that Mario Puzo’s genre-defining novel has had on popular culture since its publication in 1969. From Francis Ford Coppola’s phenomenally successful film trilogy and a slew of deferential movies and television series, to gangland parlance and schoolyard banter, its legacy is multiple and global. This glorious new edition is packed with carefully considered design details that pay homage to Puzo’s epic story and its era. From the bleeding upside-down New York skyline on the binding to the series of dramatic tableaux and portraits by award-winning illustrator Robert Carter, and Jonathan Freedland’s new introduction, this is the edition that every Godfather aficionado has been waiting for.', 179, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (40, N'Crooked House', N'CRKHSE0018.png', 13, N'A mysterious poisoning. A house full of eccentric characters. And one amateur sleuth racing to fit all of the crooked pieces together. Crooked House is the epitome of an Agatha Christie novel – in fact, Christie herself described it as one of her own ‘special favourites’. Acknowledged the world over as the undisputed queen of crime, Christie composed the plot of Crooked House with all her usual brilliance, teasing the reader with multiple red herrings and canny psychological insights before leading them to a resolution that still has the power to shock, even today. This edition features seven evocative colour illustrations by artist Sally Dunne that perfectly capture the 1940s period and country-house setting, as well as a binding design that, like Christie’s writing, is both beautiful and sinister.', 129, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (41, N'The Murder of Roger Ackroyd', N'MDRRGA0019.png', 13, N'Proclaimed by the Crime Writers’ Association as ‘the finest example of the genre ever penned’, The Murder of Roger Ackroyd contains one of the most celebrated twists in crime fiction and is consistently voted among Agatha Christie’s best novels. It is, famously, the Poirot novel that demands to be read twice: the curious reader cannot resist re-examining what they thought they knew. Laura Thompson, Christie’s biographer, described it as ‘masterly: deceptive in every way’, and it remains a rare treat for those who savour the challenge of a whodunnit, yet love to be taken in by a master of her craft.', 129, 0)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (42, N'Five Little Pigs', N'FVLTPG0020.png', 13, N'As a trained apothecary’s assistant who worked for the Red Cross during the First World War, it’s no coincidence that Agatha Christie chose poisoning as the cause of death for many of her fictional victims. This time it’s the turn of artist and serial philanderer Amyas Crale, and his long-suffering wife Caroline is convicted of his murder. When the case is revisited sixteen years later, Poirot’s instinct for injustice is piqued and he gathers fresh testimonies from the only other possible suspects – the Five Little Pigs. Widely regarded as one of Christie’s greatest murder mysteries and one of Poirot’s most compelling cases, in this gorgeous new Folio edition Five Little Pigs is illustrated by award-winning artist Andrew Davidson, including a striking silhouette binding design that offers a tantalising clue to the case.', 129, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (43, N'And Then There Were None', N'ATTWNN0021.png', 13, N'Ten people are invited to an island by a host that none of them has met. A recorded voice accuses each of them of a crime for which they were never punished. And then the dying begins. Cut off from the world, there is no escape from each other, or from themselves. And where no one is innocent, anyone might be the murderer, or the next victim …', 129, 1)
INSERT [dbo].[Book] ([id], [title], [image_url], [author_id], [description], [price], [status]) VALUES (44, N'Miss Maple Short Stories', N'MMPSST0022.png', 13, N'When a group of friends in a small village – artists, writers and clergymen – decide to tell unsolved mysteries to each other at Miss Jane Marple’s house, they expect little in the way of a contribution from their elderly host. So when she lifts her eyes from her knitting to deduce the truth about a case of domestic poisoning, they are flabbergasted. As the stories in this collection are told – disappearing bloodstains, the cryptic last message of a poisoned man, a spiritualist who predicts death – Miss Marple’s reputation grows, and before long she is being asked for help by the police. This complete collection features all 20 short stories, including ‘The Tuesday Night Club’, ‘A Christmas Tragedy’ and ‘The Case of the Perfect Maid’. They are neatly encapsulated by Henry Clithering’s cry at the solution of ‘Ingots of Gold’: ‘Miss Marple, you are wonderful!’', 129, 0)
SET IDENTITY_INSERT [dbo].[Book] OFF
/****** Object:  Table [dbo].[Book_Genre]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book_Genre](
	[book_id] [int] NOT NULL,
	[genre_id] [int] NOT NULL,
 CONSTRAINT [PK_Book_Genre] PRIMARY KEY CLUSTERED 
(
	[book_id] ASC,
	[genre_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (23, 1)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (23, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (24, 3)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (24, 4)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (24, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (25, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (25, 7)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (26, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (26, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (27, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (27, 7)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (27, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (28, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (28, 7)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (29, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (29, 7)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (30, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (30, 7)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (31, 1)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (31, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (31, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (32, 1)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (32, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (32, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (33, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (33, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (33, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (33, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (34, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (34, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (34, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (34, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (35, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (35, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (35, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (35, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (37, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (37, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (37, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (37, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (38, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (38, 4)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (38, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (38, 8)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (39, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (39, 5)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (40, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (40, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (41, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (41, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (42, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (42, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (43, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (43, 6)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (44, 1)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (44, 2)
INSERT [dbo].[Book_Genre] ([book_id], [genre_id]) VALUES (44, 6)
/****** Object:  Table [dbo].[Bill]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[address] [nvarchar](200) NOT NULL,
	[telephone] [nvarchar](20) NOT NULL,
	[date] [date] NOT NULL,
	[payment] [nvarchar](20) NOT NULL,
	[status] [nvarchar](20) NOT NULL,
	[total] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bill] ON
INSERT [dbo].[Bill] ([id], [user_id], [address], [telephone], [date], [payment], [status], [total]) VALUES (1, 2, N'69 Dinh Dong ', N'0911113052', CAST(0x29430B00 AS Date), N'cash', N'Unseen', 149)
INSERT [dbo].[Bill] ([id], [user_id], [address], [telephone], [date], [payment], [status], [total]) VALUES (2, 2, N'69 Dinh Dong ', N'0911113052', CAST(0x29430B00 AS Date), N'cash', N'Unseen', 149)
INSERT [dbo].[Bill] ([id], [user_id], [address], [telephone], [date], [payment], [status], [total]) VALUES (3, 2, N'45 Maria Ochodua', N'12344989', CAST(0x29430B00 AS Date), N'bank', N'Unseen', 3995)
INSERT [dbo].[Bill] ([id], [user_id], [address], [telephone], [date], [payment], [status], [total]) VALUES (4, 2, N'16 Dinh Dong', N'09112345678', CAST(0x34430B00 AS Date), N'bank', N'Unseen', 278)
SET IDENTITY_INSERT [dbo].[Bill] OFF
/****** Object:  Table [dbo].[Stock]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[book_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[book_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (23, 34)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (24, 12)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (25, 7)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (26, 15)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (27, 32)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (28, 14)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (29, 5)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (30, 7)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (31, 3)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (32, 27)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (33, 14)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (34, 2)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (35, 0)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (37, 12)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (38, 9)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (39, 10)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (40, 21)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (41, 12)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (42, 15)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (43, 25)
INSERT [dbo].[Stock] ([book_id], [quantity]) VALUES (44, 13)
/****** Object:  Table [dbo].[Review]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[book_id] [int] NOT NULL,
	[user_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orderline]    Script Date: 11/09/2021 20:39:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orderline](
	[bill_id] [int] NOT NULL,
	[book_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_Orderline] PRIMARY KEY CLUSTERED 
(
	[book_id] ASC,
	[bill_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Orderline] ([bill_id], [book_id], [quantity]) VALUES (3, 38, 5)
INSERT [dbo].[Orderline] ([bill_id], [book_id], [quantity]) VALUES (2, 42, 1)
INSERT [dbo].[Orderline] ([bill_id], [book_id], [quantity]) VALUES (4, 44, 2)
/****** Object:  Default [DF__User__avatar_url__22AA2996]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[User] ADD  DEFAULT ('https://avatars.githubusercontent.com/u/17879520?v=4') FOR [avatar_url]
GO
/****** Object:  ForeignKey [FK__User__role_id__24927208]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([id])
GO
/****** Object:  ForeignKey [FK__Book__author_id__239E4DCF]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Book]  WITH CHECK ADD FOREIGN KEY([author_id])
REFERENCES [dbo].[Author] ([id])
GO
/****** Object:  ForeignKey [FK__Book_Genr__book___267ABA7A]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Book_Genre]  WITH CHECK ADD FOREIGN KEY([book_id])
REFERENCES [dbo].[Book] ([id])
GO
/****** Object:  ForeignKey [FK__Book_Genr__genre__276EDEB3]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Book_Genre]  WITH CHECK ADD FOREIGN KEY([genre_id])
REFERENCES [dbo].[Genre] ([id])
GO
/****** Object:  ForeignKey [FK__Bill__user_id__2B3F6F97]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
/****** Object:  ForeignKey [FK__Stock__book_id__25869641]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD FOREIGN KEY([book_id])
REFERENCES [dbo].[Book] ([id])
GO
/****** Object:  ForeignKey [FK__Review__book_id__286302EC]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([book_id])
REFERENCES [dbo].[Book] ([id])
GO
/****** Object:  ForeignKey [FK__Review__user_id__2C3393D0]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[User] ([id])
GO
/****** Object:  ForeignKey [FK__Orderline__bill___29572725]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Orderline]  WITH CHECK ADD FOREIGN KEY([bill_id])
REFERENCES [dbo].[Bill] ([id])
GO
/****** Object:  ForeignKey [FK__Orderline__book___2A4B4B5E]    Script Date: 11/09/2021 20:39:19 ******/
ALTER TABLE [dbo].[Orderline]  WITH CHECK ADD FOREIGN KEY([book_id])
REFERENCES [dbo].[Book] ([id])
GO

CREATE TABLE IF NOT EXISTS "MigrationHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK_MigrationHistory" PRIMARY KEY ("MigrationId")
);


DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE TABLE "Category" (
        "Id" serial NOT NULL,
        "LinkName" character varying(100) NOT NULL,
        "DisplayName" text NOT NULL,
        CONSTRAINT "PK_Category" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE TABLE "Tag" (
        "Id" serial NOT NULL,
        "LinkName" character varying(100) NOT NULL,
        "DisplayName" text NOT NULL,
        CONSTRAINT "PK_Tag" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE TABLE "Post" (
        "Id" serial NOT NULL,
        "Title" text NULL,
        "Excerpt" text NULL,
        "Content" text NULL,
        "LinkName" text NULL,
        "PublishedOn" timestamp without time zone NOT NULL,
        "CategoryId" integer NULL,
        CONSTRAINT "PK_Post" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Post_Category_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "Category" ("Id") ON DELETE RESTRICT
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE TABLE "PostMetadata" (
        "PostId" integer NOT NULL,
        "Key" text NOT NULL,
        "Value" text NULL,
        CONSTRAINT "PK_PostMetadata" PRIMARY KEY ("PostId", "Key"),
        CONSTRAINT "FK_PostMetadata_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE TABLE "PostTag" (
        "PostId" integer NOT NULL,
        "TagId" integer NOT NULL,
        CONSTRAINT "PK_PostTag" PRIMARY KEY ("PostId", "TagId"),
        CONSTRAINT "FK_PostTag_Post_PostId" FOREIGN KEY ("PostId") REFERENCES "Post" ("Id") ON DELETE CASCADE,
        CONSTRAINT "FK_PostTag_Tag_TagId" FOREIGN KEY ("TagId") REFERENCES "Tag" ("Id") ON DELETE CASCADE
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE UNIQUE INDEX "IX_Category_DisplayName" ON "Category" ("DisplayName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_Category_Id" ON "Category" ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE UNIQUE INDEX "IX_Category_LinkName" ON "Category" ("LinkName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_Post_CategoryId" ON "Post" ("CategoryId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_Post_Id" ON "Post" ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE UNIQUE INDEX "IX_Post_LinkName" ON "Post" ("LinkName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_Post_PublishedOn" ON "Post" ("PublishedOn");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_PostMetadata_PostId_Key" ON "PostMetadata" ("PostId", "Key");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_PostTag_PostId" ON "PostTag" ("PostId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_PostTag_TagId" ON "PostTag" ("TagId");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE UNIQUE INDEX "IX_Tag_DisplayName" ON "Tag" ("DisplayName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE INDEX "IX_Tag_Id" ON "Tag" ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    CREATE UNIQUE INDEX "IX_Tag_LinkName" ON "Tag" ("LinkName");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190804135514_Init') THEN
    INSERT INTO "MigrationHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190804135514_Init', '3.1.1');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190908103307_MadePostLinkNameRequired') THEN
    ALTER TABLE "Post" ALTER COLUMN "LinkName" TYPE character varying(100);
    ALTER TABLE "Post" ALTER COLUMN "LinkName" SET NOT NULL;
    ALTER TABLE "Post" ALTER COLUMN "LinkName" DROP DEFAULT;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190908103307_MadePostLinkNameRequired') THEN
    INSERT INTO "MigrationHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190908103307_MadePostLinkNameRequired', '3.1.1');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190914062023_AddImageTable') THEN
    CREATE TABLE "Image" (
        "Id" serial NOT NULL,
        "Path" text NOT NULL,
        "FileName" text NOT NULL,
        "Description" text NULL,
        "UploadTime" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_Image" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190914062023_AddImageTable') THEN
    CREATE INDEX "IX_Image_Id" ON "Image" ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190914062023_AddImageTable') THEN
    CREATE INDEX "IX_Image_UploadTime" ON "Image" ("UploadTime");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20190914062023_AddImageTable') THEN
    INSERT INTO "MigrationHistory" ("MigrationId", "ProductVersion")
    VALUES ('20190914062023_AddImageTable', '3.1.1');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    DROP INDEX "IX_Post_LinkName";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    ALTER TABLE "Tag" ALTER COLUMN "Id" TYPE integer;
    ALTER TABLE "Tag" ALTER COLUMN "Id" SET NOT NULL;
    ALTER SEQUENCE "Tag_Id_seq" RENAME TO "Tag_Id_old_seq";
    ALTER TABLE "Tag" ALTER COLUMN "Id" DROP DEFAULT;
    ALTER TABLE "Tag" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY;
    SELECT * FROM setval('"Tag_Id_seq"', nextval('"Tag_Id_old_seq"'), false);
    DROP SEQUENCE "Tag_Id_old_seq";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    ALTER TABLE "Post" ALTER COLUMN "Id" TYPE integer;
    ALTER TABLE "Post" ALTER COLUMN "Id" SET NOT NULL;
    ALTER SEQUENCE "Post_Id_seq" RENAME TO "Post_Id_old_seq";
    ALTER TABLE "Post" ALTER COLUMN "Id" DROP DEFAULT;
    ALTER TABLE "Post" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY;
    SELECT * FROM setval('"Post_Id_seq"', nextval('"Post_Id_old_seq"'), false);
    DROP SEQUENCE "Post_Id_old_seq";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    ALTER TABLE "Post" ADD "Type" integer NOT NULL DEFAULT 0;
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    ALTER TABLE "Image" ALTER COLUMN "Id" TYPE integer;
    ALTER TABLE "Image" ALTER COLUMN "Id" SET NOT NULL;
    ALTER SEQUENCE "Image_Id_seq" RENAME TO "Image_Id_old_seq";
    ALTER TABLE "Image" ALTER COLUMN "Id" DROP DEFAULT;
    ALTER TABLE "Image" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY;
    SELECT * FROM setval('"Image_Id_seq"', nextval('"Image_Id_old_seq"'), false);
    DROP SEQUENCE "Image_Id_old_seq";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    ALTER TABLE "Category" ALTER COLUMN "Id" TYPE integer;
    ALTER TABLE "Category" ALTER COLUMN "Id" SET NOT NULL;
    ALTER SEQUENCE "Category_Id_seq" RENAME TO "Category_Id_old_seq";
    ALTER TABLE "Category" ALTER COLUMN "Id" DROP DEFAULT;
    ALTER TABLE "Category" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY;
    SELECT * FROM setval('"Category_Id_seq"', nextval('"Category_Id_old_seq"'), false);
    DROP SEQUENCE "Category_Id_old_seq";
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    CREATE UNIQUE INDEX "IX_Post_LinkName_Type" ON "Post" ("LinkName", "Type");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20191206025734_AddPostTypeToBlogPostTable') THEN
    INSERT INTO "MigrationHistory" ("MigrationId", "ProductVersion")
    VALUES ('20191206025734_AddPostTypeToBlogPostTable', '3.1.1');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200218174248_AddSettingTable') THEN
    CREATE TABLE "Setting" (
        "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
        "Code" text NOT NULL,
        "Value" text NULL,
        CONSTRAINT "PK_Setting" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200218174248_AddSettingTable') THEN
    CREATE UNIQUE INDEX "IX_Setting_Code" ON "Setting" ("Code");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200218174248_AddSettingTable') THEN
    CREATE INDEX "IX_Setting_Id" ON "Setting" ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200218174248_AddSettingTable') THEN
    INSERT INTO "MigrationHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200218174248_AddSettingTable', '3.1.1');
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200220161758_AddSiteLinkTable') THEN
    CREATE TABLE "SiteLink" (
        "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY,
        "Name" text NULL,
        "Link" text NULL,
        CONSTRAINT "PK_SiteLink" PRIMARY KEY ("Id")
    );
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200220161758_AddSiteLinkTable') THEN
    CREATE INDEX "IX_SiteLink_Id" ON "SiteLink" ("Id");
    END IF;
END $$;

DO $$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "MigrationHistory" WHERE "MigrationId" = '20200220161758_AddSiteLinkTable') THEN
    INSERT INTO "MigrationHistory" ("MigrationId", "ProductVersion")
    VALUES ('20200220161758_AddSiteLinkTable', '3.1.1');
    END IF;
END $$;

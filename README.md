# SimpleBlog

## how to set google auth keys:

set ClientId and Secret to your local machine User-secrets:

`> dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"`

`> dotnet user-secrets set "Authentication:Google:ClientSecret" "<secret>"`

---

## Data structrue

This project is designed as a CMS (Content Management System) to manage various types of content, including weblog posts.

In a typical CMS, there are usually multiple content types such as weblog posts, post categories, products, etc. Each of these requires a unique, SEO-friendly URL.

To support this, the project introduces a master class named `Content`, which represents any type of content included on the website. A unique URL identifier, defined as a property named `Token`, ensures that URLs remain globally unique and SEO-friendly.

Users can access content through a clean, SEO-optimized URL like `https://yoursite.com/some-unique-token`, without needing to know the specific content type.

An additional advantage of this structure is the ability to store a single piece of content as a "master" class with multiple versions of detailed entities. (See the "Versioning Strategies" section for more information.)

## Versioning strategies

There are multiple strategies for storing and retrieving different versions of a single piece of content.

One approach is to designate an entity as the main `WeblogPost`, with all other versions referencing it via a foreign key column such as `MainWeblogId`.

When updating a weblog post, the main entity is treated as the active version, while older versions are stored as separate entities that reference the main post. In this scenario, the process involves copying the current active version, saving the copy as a historical record, and then updating the active version. This requires both an `INSERT` query to store the historical version and an `UPDATE` query to modify the current version.

The history of a weblog post can be retrieved using a shared property like `MainWeblogId`.

(fetch all entities with same `MainWeblogId` and the MainWeblog itself.)



Another approach (applied to this project) is to use the `Content` entity as the master and insert a new `BlogPost` record on every update. In this model, older entities automatically become historical records without needing to be modified. Only an `INSERT` query is needed to store the latest version as the current one.

There is a flag property named `ActiveVersion` in the `BlogPost` class. Technically, this flag isn’t necessary for identifying the latest version, since the incremental `Id` and `CreateDate` property already indicates when each record was created and which record is the most recent.

However, while the latest version could be found using an aggregate function to retrieve the record with the most recent `CreateDate`, using the `ActiveVersion` flag allows for faster `SELECT` queries limited to `ActiveVersion` records only and more efficient data retrieval without scanning all similar records.

in this scenario, The history of a weblog post can be retrieved using `ContentId` as the shared property.

(fetch all entities with same `ContentId` which already includes Active version of blogPost entity)

---

## API section

There is an area in this project named as `Api` which is public endpoints for `GET` action to fetch list of blog posts and `GET` action to feth a single blogPost using `Id` parameter.

for now, there is no implementation for	`POST`, `PUT` and `DELETE` methods. because this kind of actions needs autorized access and need to provide an authentication method like jwt.
create table style_item_product_sub_category
(
    style_item_product_sub_category_id bigint generated by default as identity
        primary key,
    style_item_product_id              bigint                   not null
        constraint product_category
            references style_item_product
            on delete cascade,
    sub_category_name                  varchar                  not null,
    added_by                           bigint                   not null,
    date_added                         timestamp with time zone not null,
    updated_by                         bigint,
    date_updated                       timestamp with time zone
);

alter table style_item_product_sub_category
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on style_item_product_sub_category to anon;

grant delete, insert, references, select, trigger, truncate, update on style_item_product_sub_category to authenticated;

grant delete, insert, references, select, trigger, truncate, update on style_item_product_sub_category to service_role;


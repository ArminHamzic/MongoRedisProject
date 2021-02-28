package at.htl.entity

data class Schwammal(
        var key: String,
        val firstName: String,
        val lastName: String,
        val poolEdgeSwimmer: Boolean,
        var schoolSubjects: MutableList<SchoolSubject>
)
